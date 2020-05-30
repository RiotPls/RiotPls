using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using RiotPls.DataDragon.Entities;
using RiotPls.DataDragon.Enums;
using RiotPls.DataDragon.Extensions;

namespace RiotPls.DataDragon
{
    public sealed partial class DataDragonClient : IDataDragonClient
    {
        public const string DataDragonUrl = "https://ddragon.leagueoflegends.com/";
        public const string CommunityDragonUrl = "https://cdn.communitydragon.org/";

        public Language DefaultLanguage => _options.DefaultLanguage;

        private readonly DataDragonClientOptions _options;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly SemaphoreSlim _semaphore;
        private readonly object _lock;
        
        private ImmutableArray<GameVersion> _versions;
        private ImmutableArray<Language> _languages;
        private GameVersion? _latestVersion;
        private volatile bool _isDisposed;

        public DataDragonClient() : this(DataDragonClientOptions.Default)
        {
        }

        public DataDragonClient(DataDragonClientOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _client = new HttpClient
            {
                BaseAddress = new Uri(DataDragonUrl)
            };
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _lock = new object();
            _semaphore = new SemaphoreSlim(1, 1);
            _latestVersion = null;
        }

        ~DataDragonClient()
        {
            Dispose();
        }

        /// <inheritdoc/>
        public ValueTask<IReadOnlyList<GameVersion>?> GetVersionsAsync(CancellationToken token = default)
        {
            ThrowIfDisposed();

            lock (_lock)
            {
                if (_options.CacheMode != CacheMode.None && !_languages.IsDefaultOrEmpty)
                    return new ValueTask<IReadOnlyList<GameVersion>?>(_versions);

                return new ValueTask<IReadOnlyList<GameVersion>?>(FetchVersionsAsync(token));
            }
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<GameVersion>?> FetchVersionsAsync(CancellationToken token = default)
        {
            ThrowIfDisposed();

            try
            {
                await _semaphore.WaitAsync(token).ConfigureAwait(false);

                var versions = (await MakeRequestAsync<GameVersion[]>(
                    "api/versions.json", token).ConfigureAwait(false)).ToImmutableArray();

                if (_options.CacheMode != CacheMode.None)
                    _versions = versions;

                return versions;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        /// <inheritdoc/>
        public Task<GameVersion?> FetchLatestVersionAsync(CancellationToken token = default)
        {
            ThrowIfDisposed();

            lock (_lock)
                return InternalFetchLatestVersionAsync(token);
        }

        /// <inheritdoc/>
        public ValueTask<IReadOnlyList<Language>?> GetLanguagesAsync(CancellationToken token = default)
        {
            ThrowIfDisposed();

            lock (_lock)
            {
                if (_options.CacheMode != CacheMode.None && !_languages.IsDefaultOrEmpty)
                    return new ValueTask<IReadOnlyList<Language>?>(_languages);

                return new ValueTask<IReadOnlyList<Language>?>(FetchLanguagesAsync(token));
            }
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<Language>?> FetchLanguagesAsync(CancellationToken token = default)
        {
            ThrowIfDisposed();

            try
            {
                await _semaphore.WaitAsync(token).ConfigureAwait(false);

                var data = await MakeRequestAsync<Language[]>(
                    "cdn/languages.json", token).ConfigureAwait(false);

                if (data is null)
                    return null;

                var languages = data.ToImmutableArray();

                if (_options.CacheMode != CacheMode.None)
                    _languages = languages;

                return languages;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        /// <inheritdoc/>
        public async IAsyncEnumerable<Champion> GetChampionsAsync(
            GameVersion version,
            Language? language = null,
            [EnumeratorCancellation] CancellationToken token = default)
        {
            ThrowIfDisposed();

            if (version is null)
                throw new ArgumentNullException(nameof(version));

            language ??= DefaultLanguage;

            if (_options.CacheMode == CacheMode.None ||
                !Cache<ChampionFullData>.Instance.TryGetValue((version, language.Value), out var data) ||
                _options.CacheMode == CacheMode.MostRecentOnly && data.Version < _latestVersion)
            {
                await foreach (var champion in FetchChampionsAsync(version, language, token).ConfigureAwait(false))
                    yield return champion;
            }
            else 
            {
                lock (_lock)
                {
                    foreach (var champion in data.Champions.Values)
                    {
                        if (token.IsCancellationRequested)
                            yield break;

                        yield return champion;
                    }
                }
            }
        }

        /// <inheritdoc/>
        public async IAsyncEnumerable<Champion> FetchChampionsAsync(
            GameVersion? version = null,
            Language? language = null,
            [EnumeratorCancellation] CancellationToken token = default)
        {
            ThrowIfDisposed();

            try
            {
                await _semaphore.WaitAsync(token).ConfigureAwait(false);

                var latestVersion = await InternalFetchLatestVersionAsync(token).ConfigureAwait(false);

                language ??= DefaultLanguage;
                version ??= latestVersion;

                if (version is null)
                    yield break;

                if (version > latestVersion)
                    throw new ArgumentOutOfRangeException(
                        nameof(version),
                        "The provided version is higher than the latest Data Dragon version.");

                var data = await MakeRequestAsync<ChampionFullDataDto>(
                    GetEndpoint<ChampionFullData>(version, language.Value.GetCode(), default),
                    token).ConfigureAwait(false);

                if (data is null || token.IsCancellationRequested)
                    yield break;

                var fullData = new ChampionFullData(data, true);

                if (_options.CacheMode != CacheMode.None)
                {
                    if (_options.CacheMode == CacheMode.MostRecentOnly && latestVersion == version)
                        Cache<ChampionFullData>.Instance.Clear();

                    Cache<ChampionFullData>.Instance[version, language.Value] = fullData;

                    foreach (var championDto in data.Champions.Values)
                    {
                        if (token.IsCancellationRequested)
                            yield break;

                        var champion = new Champion(championDto, version);

                        if (_options.CacheMode != CacheMode.None)
                            fullData.Champions.TryAdd(champion.Id, champion);

                        yield return champion;
                    }
                }
                else
                {
                    foreach (var championDto in data.Champions.Values)
                    {
                        if (token.IsCancellationRequested)
                            yield break;

                        yield return new Champion(championDto, version);
                    }
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        /// <inheritdoc/>
        public async ValueTask<Champion?> GetChampionAsync(
            ChampionId championId,
            GameVersion version,
            Language? language = null,
            CancellationToken token = default)
        {
            var data = await GetBaseDataAsync<ChampionFullDataDto, ChampionFullData>(version, language, championId)
                .ConfigureAwait(false);
            return data?.Champions[championId];
        }

        /// <inheritdoc/>
        public async Task<Champion?> FetchChampionAsync(
            ChampionId championId,
            GameVersion? version = null,
            Language? language = null,
            CancellationToken token = default)
        {
            var data = await FetchBaseDataAsync<ChampionFullDataDto, ChampionFullData>(version, language, championId)
                .ConfigureAwait(false);
            return data?.Champions[championId];
        }

        /// <inheritdoc/>
        public async ValueTask<IReadOnlyCollection<SummonerSpell>?> GetSummonerSpellsAsync(
            GameVersion version,
            Language? language = null,
            CancellationToken token = default)
        {
            var data = await GetBaseDataAsync<SummonerSpellDataDto, SummonerSpellData>(version, language)
                .ConfigureAwait(false);
            return data?.SummonerSpells.Values;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<SummonerSpell>?> FetchSummonerSpellsAsync(
            GameVersion? version = null,
            Language? language = null,
            CancellationToken token = default)
        {
            var data = await FetchBaseDataAsync<SummonerSpellDataDto, SummonerSpellData>(version, language)
                .ConfigureAwait(false);
            return data?.SummonerSpells.Values;
        }

        /// <inheritdoc/>
        public async ValueTask<IReadOnlyCollection<ProfileIcon>?> GetProfileIconsAsync(
            GameVersion version,
            Language? language = null,
            CancellationToken token = default)
        {
            var data = await GetBaseDataAsync<ProfileIconDataDto, ProfileIconData>(version, language)
                .ConfigureAwait(false);
            return data?.ProfileIcons.Values;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<ProfileIcon>?> FetchProfileIconsAsync(
            GameVersion? version = null,
            Language? language = null,
            CancellationToken token = default)
        {
            var data = await FetchBaseDataAsync<ProfileIconDataDto, ProfileIconData>(version, language)
                .ConfigureAwait(false);
            return data?.ProfileIcons.Values;
        }

        /// <inheritdoc/>
        public async ValueTask<IReadOnlyCollection<Map>?> GetMapsAsync(
            GameVersion version,
            Language? language = null,
            CancellationToken token = default)
        {
            var data = await GetBaseDataAsync<MapDataDto, MapData>(version, language).ConfigureAwait(false);
            return data?.Maps.Values;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<Map>?> FetchMapsAsync(
            GameVersion? version = null,
            Language? language = null,
            CancellationToken token = default)
        {
            var data = await FetchBaseDataAsync<MapDataDto, MapData>(version, language).ConfigureAwait(false);
            return data?.Maps.Values;
        }

        /// <inheritdoc/>
        public ValueTask<IReadOnlyList<Rune>?> GetRunesAsync(
            GameVersion version,
            Language? language = null,
            CancellationToken token = default)
        {
            ThrowIfDisposed();

            if (version is null)
                throw new ArgumentNullException(nameof(version));

            lock (_lock)
            {
                language ??= DefaultLanguage;

                if (_options.CacheMode == CacheMode.None ||
                    _options.CacheMode == CacheMode.MostRecentOnly && version < _latestVersion ||
                    !Cache<IReadOnlyList<Rune>>.Instance.TryGetValue((version, language.Value), out var data))
                    return new ValueTask<IReadOnlyList<Rune>?>(FetchRunesAsync(version, language.Value));

                return new ValueTask<IReadOnlyList<Rune>?>(data);
            }
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<Rune>?> FetchRunesAsync(
            GameVersion? version = null,
            Language? language = null,
            CancellationToken token = default)
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
                    throw new ArgumentOutOfRangeException(nameof(version),
                        "The provided version is higher than the latest Data Dragon version.");

                var data = await MakeRequestAsync<IReadOnlyList<RuneDto>, IReadOnlyList<Rune>>(
                    $"cdn/{version}/data/{language.Value.GetCode()}/runesReforged.json",
                    dtos => dtos.Select(x => new Rune(x)).ToImmutableArray(),
                    token).ConfigureAwait(false);

                if (data is null)
                    return null;

                if (_options.CacheMode != CacheMode.None)
                {
                    if (_options.CacheMode == CacheMode.MostRecentOnly && version == latestVersion)
                        Cache<IReadOnlyList<Rune>>.Instance.Clear();

                    Cache<IReadOnlyList<Rune>>.Instance[version, language.Value] = data;
                }

                return data;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        /// <inheritdoc/>
        public async ValueTask<IReadOnlyCollection<MissionAsset>?> GetMissionAssetsAsync(
            GameVersion version,
            Language? language = null,
            CancellationToken token = default)
        {
            var data = await GetBaseDataAsync<MissionAssetDataDto, MissionAssetData>(version, language)
                .ConfigureAwait(false);
            return data?.MissionAssets.Values;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<MissionAsset>?> FetchMissionAssetsAsync(
            GameVersion? version = null,
            Language? language = null,
            CancellationToken token = default)
        {
            var data = await FetchBaseDataAsync<MissionAssetDataDto, MissionAssetData>(version, language)
                .ConfigureAwait(false);
            return data?.MissionAssets.Values;
        }
        
        /// <inheritdoc/>
        public async ValueTask<IReadOnlyCollection<Item>?> GetItemsAsync(
            GameVersion version,
            Language? language = null,
            CancellationToken token = default)
        {
            var data = await GetBaseDataAsync<ItemDataDto, ItemData>(version, language)
                .ConfigureAwait(false);
            return data?.Items.Values;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<Item>?> FetchItemsAsync(
            GameVersion? version = null,
            Language? language = null,
            CancellationToken token = default)
        {
            var data = await FetchBaseDataAsync<ItemDataDto, ItemData>(version, language)
                .ConfigureAwait(false);

            foreach (var item in data.Items.Values)
            {
                item.From = item.FromIds.Select(x => data.Items[x]).ToImmutableArray();
                item.Into = item.IntoIds.Select(x => data.Items[x]).ToImmutableArray();
            }
            
            return data?.Items.Values;
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;

            lock (_lock)
            {
                _client.CancelPendingRequests();
                _client.Dispose();
                _isDisposed = true;

                GC.SuppressFinalize(this);
            }
        }
    }
}