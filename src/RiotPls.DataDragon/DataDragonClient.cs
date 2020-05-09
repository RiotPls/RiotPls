using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using RiotPls.DataDragon.Entities;
using RiotPls.DataDragon.Enums;
using RiotPls.DataDragon.Helpers;

namespace RiotPls.DataDragon
{
    public sealed partial class DataDragonClient //: IDataDragonClient
    {
        public const string Host = "https://ddragon.leagueoflegends.com";
        public const string Api = "/api";
        public const string Cdn = "/cdn";

        public Language DefaultLanguage => _options.DefaultLanguage;

        internal static DataDragonClient Instance { get; } = new DataDragonClient(DataDragonClientOptions.Default);

        private readonly DataDragonClientOptions _options;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly SemaphoreSlim _semaphore;
        private readonly object _lock;
        private ImmutableArray<GameVersion> _versions;
        private ImmutableArray<Language> _languages;
        private bool _isDisposed;

        public DataDragonClient() : this(DataDragonClientOptions.Default)
        {
        }

        public DataDragonClient(DataDragonClientOptions options)
        {
            if (options is null)
                throw new ArgumentNullException(nameof(options));

            _options = options;
            _client = new HttpClient
            {
                BaseAddress = new Uri(Host)
            };
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _lock = new object();
            _semaphore = new SemaphoreSlim(1, 1);
        }

        /// <inheritdoc/>
        public ValueTask<IReadOnlyList<GameVersion>> GetVersionsAsync()
        {
            ThrowIfDisposed();

            lock (_lock)
                return ValueTaskHelper.Create(
                    this,
                    !_versions.IsDefaultOrEmpty,
                    client => client._versions,
                    async client => await client.FetchVersionsAsync().ConfigureAwait(false));
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<GameVersion>> FetchVersionsAsync()
        {
            ThrowIfDisposed();

            try
            {
                await _semaphore.WaitAsync().ConfigureAwait(false);

                var versions = (await MakeRequestAsync<GameVersion[]>(
                    $"{Api}/versions.json").ConfigureAwait(false)).ToImmutableArray();

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
        public async Task<GameVersion> FetchLatestVersionAsync()
        {
            ThrowIfDisposed();

            try
            {
                await _semaphore.WaitAsync().ConfigureAwait(false);
                return await InternalFetchLatestVersionAsync().ConfigureAwait(false);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        /// <inheritdoc/>
        public ValueTask<IReadOnlyList<Language>> GetLanguagesAsync()
        {
            ThrowIfDisposed();

            lock (_lock)
                return ValueTaskHelper.Create(
                    this,
                    _options.CacheMode != CacheMode.None && !_languages.IsDefaultOrEmpty,
                    client => client._languages,
                    async client => await client.FetchLanguagesAsync().ConfigureAwait(false));
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<Language>> FetchLanguagesAsync()
        {
            ThrowIfDisposed();

            try
            {
                await _semaphore.WaitAsync().ConfigureAwait(false);

                var languages = (await MakeRequestAsync<Language[]>(
                    $"{Cdn}/languages.json").ConfigureAwait(false)).ToImmutableArray();

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
        public ValueTask<ChampionBaseData> GetPartialChampionsAsync(GameVersion version, Language? language = null)
            => GetBaseDataAsync<ChampionBaseDataDto, ChampionBaseData>(version, language);

        /// <inheritdoc/>
        public Task<ChampionBaseData> FetchPartialChampionAsync(GameVersion? version = null, Language? language = null)
            => FetchBaseDataAsync<ChampionBaseDataDto, ChampionBaseData>(version, language);

        /// <inheritdoc/>
        public ValueTask<ChampionFullData> GetChampionsAsync(GameVersion version)
            => GetBaseDataAsync<ChampionFullDataDto, ChampionFullData>(version, DefaultLanguage);

        /// <inheritdoc/>
        public ValueTask<ChampionFullData> GetChampionsAsync(GameVersion version, Language language)
            => GetBaseDataAsync<ChampionFullDataDto, ChampionFullData>(version, language);

        /// <inheritdoc/>
        public Task<ChampionFullData> FetchChampionAsync()
            => FetchBaseDataAsync<ChampionFullDataDto, ChampionFullData>(null, DefaultLanguage);

        /// <inheritdoc/>
        public Task<ChampionFullData> FetchChampionAsync(GameVersion version)
            => FetchBaseDataAsync<ChampionFullDataDto, ChampionFullData>(version, DefaultLanguage);

        /// <inheritdoc/>
        public Task<ChampionFullData> FetchChampionAsync(GameVersion version, Language language)
            => FetchBaseDataAsync<ChampionFullDataDto, ChampionFullData>(version, language);

        /// <inheritdoc/>
        public ValueTask<ChampionData> GetChampionAsync(ChampionId championId, GameVersion version, Language? language = null)
            => GetBaseDataAsync<ChampionDataDto, ChampionData>(version, language, championId);

        /// <inheritdoc/>
        public Task<ChampionData> FetchChampionAsync(ChampionId championId, GameVersion? version = null, Language? language = null)
            => FetchBaseDataAsync<ChampionDataDto, ChampionData>(version, language, championId);

        /// <inheritdoc/>
        public ValueTask<SummonerSpellData> GetSummonerSpellsAsync(GameVersion version, Language? language = null)
            => GetBaseDataAsync<SummonerSpellDataDto, SummonerSpellData>(version, language);
    

        /// <inheritdoc/>
        public ValueTask<ProfileIconData> GetProfileIconsAsync(GameVersion version, Language? language = null)
            => GetBaseDataAsync<ProfileIconDataDto, ProfileIconData>(version, language);

        /// <inheritdoc/>
        public Task<ProfileIconData> FetchProfileIconsAsync(GameVersion version, Language? language = null)
            => FetchBaseDataAsync<ProfileIconDataDto, ProfileIconData>(version, language);

        /// <inheritdoc/>
        public ValueTask<MapData> GetMapsAsync(GameVersion version, Language? language = null)
            => GetBaseDataAsync<MapDataDto, MapData>(version, language);

        /// <inheritdoc/>
        public Task<MapData> FetchMapsAsync(GameVersion? version = null, Language? language = null)
            => FetchBaseDataAsync<MapDataDto, MapData>(version, language);

        /// <inheritdoc/>
        public ValueTask<IReadOnlyList<Rune>> GetRunesAsync(GameVersion version, Language? language = null)
        {
            ThrowIfDisposed();

            if (version is null)
                throw new ArgumentNullException(nameof(version));

            lock (_lock)
            {
                language ??= DefaultLanguage;

                if (_options.CacheMode == CacheMode.None ||
                    !Cache<IReadOnlyList<Rune>>.Instance.TryGetValue((version, language.Value), out var data))
                    return new ValueTask<IReadOnlyList<Rune>>(FetchRunesAsync(version, language.Value));

                return new ValueTask<IReadOnlyList<Rune>>(data);
            }
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<Rune>> FetchRunesAsync(GameVersion? version = null, Language? language = null)
        {
            ThrowIfDisposed();

            try
            {
                await _semaphore.WaitAsync().ConfigureAwait(false);

                var latestVersion = await FetchLatestVersionAsync().ConfigureAwait(false);

                language ??= DefaultLanguage;
                version ??= latestVersion;

                if (version > latestVersion)
                    throw new ArgumentOutOfRangeException(nameof(version), $"The providen version is higher than the latest Data Dragon version.");

                var data = await MakeRequestAsync<IReadOnlyList<RuneDto>, IReadOnlyList<Rune>>(
                    $"{Cdn}/{version}/data/{language}/runesReforged.json",
                    dtos => dtos.Select(x => new Rune(x)).ToImmutableArray()).ConfigureAwait(false);

                if (_options.CacheMode != CacheMode.None)
                    Cache<IReadOnlyList<Rune>>.Instance[version, language.Value] = data;

                return data;
            }
            catch (Exception e)
            {
                throw new DataNotFoundException($"We couldn't fetch data for the version: {version}. Read the inner exception for more details.", e);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        /// <inheritdoc/>
        public ValueTask<MissionAssetData> GetMissionAssetsAsync(GameVersion version, Language? language = null)
            => GetBaseDataAsync<MissionAssetDataDto, MissionAssetData>(version, language);

        /// <inheritdoc/>
        public Task<MissionAssetData> FetchMissionAssetsAsync(GameVersion version, Language? language = null)
            => FetchBaseDataAsync<MissionAssetDataDto, MissionAssetData>(version, language);

        public void Dispose()
        {
            lock (_lock)
            {
                if (_isDisposed)
                    return;

                _client.CancelPendingRequests();
                _client.Dispose();
                _isDisposed = true;
            }
        }
    }
}