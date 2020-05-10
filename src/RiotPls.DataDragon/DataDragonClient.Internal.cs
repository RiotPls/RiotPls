using System;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using RiotPls.DataDragon.Entities;
using RiotPls.DataDragon.Enums;
using RiotPls.DataDragon.Extensions;

namespace RiotPls.DataDragon
{
    public partial class DataDragonClient
    {
        private async Task<GameVersion> InternalFetchLatestVersionAsync()
        {
            ThrowIfDisposed();

            var data = await MakeRequestAsync<string[]>($"{Api}/versions.json").ConfigureAwait(false);
            return GameVersion.Parse(data[0]);
        }

        private ValueTask<T> GetBaseDataAsync<TDto, T>(GameVersion version, Language? language = null,
            ChampionId? championId = null)
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
                        _options.CacheMode == CacheMode.MostRecentOnly && championData.Version < version)
                        return new ValueTask<T>((Task<T>) (object)
                            FetchBaseDataAsync<ChampionDataDto, ChampionData>(version, language.Value, championId));

                    return new ValueTask<T>((T) (object) championData);
                }

                if (_options.CacheMode == CacheMode.None ||
                    !Cache<T>.Instance.TryGetValue((version, language.Value), out var data) ||
                    _options.CacheMode == CacheMode.MostRecentOnly && data.Version < version)
                    return new ValueTask<T>(FetchBaseDataAsync<TDto, T>(version, language.Value));

                return new ValueTask<T>(data);
            }
        }

        private async Task<T> FetchBaseDataAsync<TDto, T>(
            GameVersion? version, Language? language = null, ChampionId? championId = null)
            where T : BaseData
        {
            ThrowIfDisposed();

            try
            {
                await _semaphore.WaitAsync().ConfigureAwait(false);

                var latestVersion = await InternalFetchLatestVersionAsync().ConfigureAwait(false);

                language ??= DefaultLanguage;
                version ??= latestVersion;

                if (version > latestVersion)
                    throw new ArgumentOutOfRangeException(nameof(version),
                        "The provided version is higher than the latest Data Dragon version.");

                var data = await MakeRequestAsync<TDto, T>(
                    GetEndpoint<T>(version, language.Value.GetCode(), championId.GetValueOrDefault()),
                    Factory<TDto, T>.CreateInstance).ConfigureAwait(false);

                if (_options.CacheMode == CacheMode.MostRecentOnly && latestVersion.Equals(data.Version))
                    Cache<T>.Instance[latestVersion, language.Value] = data;
                else if (_options.CacheMode == CacheMode.KeepAll)
                    Cache<T>.Instance[version, language.Value] = data;

                return data;
            }
            catch (Exception e)
            {
                throw new DataNotFoundException(
                    $"We couldn't fetch data for the version: {version}. Read the inner exception for more details.",
                    e,
                    _client.BaseAddress +
                    GetEndpoint<T>(
                        version!,
                        language.GetValueOrDefault().GetCode(),
                        championId.GetValueOrDefault())[1..],
                    version!,
                    language!.Value);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private static string GetEndpoint<T>(GameVersion version, string language, ChampionId championId)
        {
            if (typeof(T) == typeof(ChampionBaseData))
                return $"{Cdn}/{version}/data/{language}/champion.json";

            if (typeof(T) == typeof(ChampionFullData))
                return $"{Cdn}/{version}/data/{language}/championFull.json";

            if (typeof(T) == typeof(ChampionData))
                return $"{Cdn}/{version}/data/{language}/champion/{championId}.json";

            if (typeof(T) == typeof(SummonerSpellData))
                return $"{Cdn}/{version}/data/{language}/summoner.json";

            if (typeof(T) == typeof(ProfileIconData))
                return $"{Cdn}/{version}/data/{language}/profileicon.json";

            if (typeof(T) == typeof(MapData))
                return $"{Cdn}/{version}/data/{language}/map.json";

            if (typeof(T) == typeof(MissionAssetData))
                return $"{Cdn}/{version}/data/{language}/mission-assets.json";

            throw new NotImplementedException($"Endpoint for the type {typeof(T).Name} has not been implemented.");
        }

        private async Task<TEntity> MakeRequestAsync<TDto, TEntity>(string url, Func<TDto, TEntity> func)
            => func(await MakeRequestAsync<TDto>(url).ConfigureAwait(false));

        private async Task<TEntity> MakeRequestAsync<TEntity>(string url)
        {
            var stream = await _client.GetStreamAsync(url).ConfigureAwait(false);
            return await JsonSerializer.DeserializeAsync<TEntity>(
                stream, _jsonSerializerOptions).ConfigureAwait(false);
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