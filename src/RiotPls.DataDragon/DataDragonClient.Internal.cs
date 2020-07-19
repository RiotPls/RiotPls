using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using RiotPls.DataDragon.Entities;
using RiotPls.DataDragon.Enums;
using RiotPls.DataDragon.Extensions;

namespace RiotPls.DataDragon
{
    public partial class DataDragonClient
    {
        private async Task<GameVersion?> InternalFetchLatestVersionAsync(CancellationToken token)
        {
            ThrowIfDisposed();

            var data = await MakeRequestAsync<string[]>($"api/versions.json", token).ConfigureAwait(false);

            if (data is null)
                return null;

            return _latestVersion = GameVersion.Parse(data[0]);
        }

        private ValueTask<T?> GetBaseDataAsync<TDto, T>(
            GameVersion version,
            Language? language = null,
            ChampionId? championId = null,
            CancellationToken token = default)
            where TDto : class
            where T : BaseData
        {
            ThrowIfDisposed();

            if (version is null)
                throw new ArgumentNullException(nameof(version));

            lock (_lock)
            {
                language ??= DefaultLanguage;

                if (championId is ChampionId id)
                {
                    if (_options.CacheMode == CacheMode.None ||
                        !ChampionCache.TryGetValue(id, out var cache) ||
                        !cache.TryGetValue((version, language.Value), out var championData) ||
                        _options.CacheMode == CacheMode.MostRecentOnly && championData.Version < _latestVersion)
                        return new ValueTask<T?>(FetchBaseDataAsync<TDto, T>(version, language.Value, championId, token));

                    return new ValueTask<T?>((T)(object)championData);
                }

                if (_options.CacheMode == CacheMode.None ||
                    !Cache<T>.Instance.TryGetValue((version, language.Value), out var data) ||
                    _options.CacheMode == CacheMode.MostRecentOnly && data.Version < _latestVersion)
                    return new ValueTask<T?>(FetchBaseDataAsync<TDto, T>(version, language.Value, token: token));

                return new ValueTask<T?>(data);
            }
        }

        private async Task<T?> FetchBaseDataAsync<TDto, T>(
            GameVersion? version,
            Language? language = null,
            ChampionId? championId = null,
            CancellationToken token = default)
            where TDto : class
            where T : BaseData
        {
            ThrowIfDisposed();

            try
            {
                await _semaphore.WaitAsync(token).ConfigureAwait(false);

                var latestVersion = await InternalFetchLatestVersionAsync(token).ConfigureAwait(false);

                language ??= DefaultLanguage;
                version ??= latestVersion;

                if (version is null)
                    return null;

                if (version > latestVersion)
                    throw new ArgumentOutOfRangeException(
                        nameof(version),
                        "The provided version is higher than the latest Data Dragon version.");

                var data = await MakeRequestAsync<TDto, T>(
                    GetEndpoint<T>(version, language.Value.GetCode(), championId.GetValueOrDefault()),
                    Factory<TDto, T>.CreateInstance, token).ConfigureAwait(false);

                if (data is null)
                    return null;

                if (_options.CacheMode != CacheMode.None)
                {
                    if (_options.CacheMode == CacheMode.MostRecentOnly && latestVersion == version)
                        Cache<T>.Instance.Clear();

                    Cache<T>.Instance[version, language.Value] = data;
                }

                return data;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private static string GetEndpoint<T>(GameVersion version, string language, ChampionId championId)
        {
            if (typeof(T) == typeof(ChampionFullData))
                return $"cdn/{version}/data/{language}/championFull.json";

            if (typeof(T) == typeof(ChampionData))
                return $"cdn/{version}/data/{language}/champion/{championId}.json";

            if (typeof(T) == typeof(SummonerSpellData))
                return $"cdn/{version}/data/{language}/summoner.json";

            if (typeof(T) == typeof(ProfileIconData))
                return $"cdn/{version}/data/{language}/profileicon.json";

            if (typeof(T) == typeof(MapData))
                return $"cdn/{version}/data/{language}/map.json";

            if (typeof(T) == typeof(MissionAssetData))
                return $"cdn/{version}/data/{language}/mission-assets.json";

            if (typeof(T) == typeof(ItemData))
                return $"cdn/{version}/data/{language}/item.json";

            throw new NotImplementedException($"Endpoint for the type {typeof(T).Name} has not been implemented.");
        }

        private async Task<TEntity?> MakeRequestAsync<TDto, TEntity>(string url, Func<TDto, TEntity> func, CancellationToken token)
            where TDto : class
            where TEntity : class
        {
            var dto = await MakeRequestAsync<TDto>(url, token).ConfigureAwait(false);

            if (dto is null)
                return null;

            return func(dto);
        }

        private async Task<TEntity?> MakeRequestAsync<TEntity>(string url, CancellationToken token)
            where TEntity : class
        {
            var response = await _client.GetAsync(url, token).ConfigureAwait(false);
 
            if (!response.IsSuccessStatusCode)
                return null;

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            return await JsonSerializer.DeserializeAsync<TEntity>(
                stream, _jsonSerializerOptions, token).ConfigureAwait(false);
        }

        private void ThrowIfDisposed()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        private static class Factory<TDto, T>
        {
            private static readonly Func<TDto, T> _func;
            public static T CreateInstance(TDto dto) => _func(dto);

            static Factory()
            {
                var parameter = Expression.Parameter(typeof(TDto));
                var constructor = typeof(T).GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.Instance,
                    null,
                    new[] { typeof(TDto) },
                    null);
                var @new = Expression.New(constructor, parameter);
                var lambda = Expression.Lambda<Func<TDto, T>>(@new, parameter);

                _func = lambda.Compile();
            }
        }
    }
}