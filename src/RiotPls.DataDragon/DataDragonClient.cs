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
using RiotPls.DataDragon.Extensions;

namespace RiotPls.DataDragon
{
    public sealed partial class DataDragonClient : IDataDragonClient
    {
        public const string Host = "https://ddragon.leagueoflegends.com";
        public const string Api = "/api";
        public const string Cdn = "/cdn";

        public Language DefaultLanguage
            => _options.DefaultLanguage;

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
            _options = options ?? throw new ArgumentNullException(nameof(options));
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

        ~DataDragonClient()
        {
            Dispose();
        }

        /// <inheritdoc/>
        public ValueTask<IReadOnlyList<GameVersion>> GetVersionsAsync()
        {
            ThrowIfDisposed();

            lock (_lock)
            {
                if (_options.CacheMode != CacheMode.None && !_languages.IsDefaultOrEmpty)
                    return new ValueTask<IReadOnlyList<GameVersion>>(_versions);

                return new ValueTask<IReadOnlyList<GameVersion>>(FetchVersionsAsync());
            }
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
        public Task<GameVersion> FetchLatestVersionAsync()
        {
            ThrowIfDisposed();

            lock (_lock)
                return InternalFetchLatestVersionAsync();
        }

        /// <inheritdoc/>
        public ValueTask<IReadOnlyList<Language>> GetLanguagesAsync()
        {
            ThrowIfDisposed();

            lock (_lock)
            {
                if (_options.CacheMode != CacheMode.None && !_languages.IsDefaultOrEmpty)
                    return new ValueTask<IReadOnlyList<Language>>(_languages);

                return new ValueTask<IReadOnlyList<Language>>(FetchLanguagesAsync());
            }
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
        public async ValueTask<IReadOnlyList<ChampionSummary>> GetChampionsSummaryAsync(GameVersion version,
            Language? language = null)
        {
            var data = await GetBaseDataAsync<ChampionFullDataDto, ChampionFullData>(version, language)
                .ConfigureAwait(false);
            return (IReadOnlyList<ChampionSummary>) data.Champions.Values; // God bless covariance
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<ChampionSummary>> FetchChampionsSummaryAsync(GameVersion? version = null,
            Language? language = null)
        {
            var data = await FetchBaseDataAsync<ChampionFullDataDto, ChampionFullData>(version, language)
                .ConfigureAwait(false);
            return (IReadOnlyList<ChampionSummary>) data.Champions.Values;
        }

        /// <inheritdoc/>
        public async ValueTask<IReadOnlyList<Champion>> GetChampionsAsync(GameVersion version,
            Language? language = null)
        {
            var data = await GetBaseDataAsync<ChampionFullDataDto, ChampionFullData>(version, language)
                .ConfigureAwait(false);
            return (IReadOnlyList<Champion>) data.Champions.Values;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<Champion>> FetchChampionsAsync(GameVersion? version = null,
            Language? language = null)
        {
            var data = await FetchBaseDataAsync<ChampionFullDataDto, ChampionFullData>(version, language)
                .ConfigureAwait(false);
            return (IReadOnlyList<Champion>) data.Champions.Values;
        }

        /// <inheritdoc/>
        public async ValueTask<ChampionSummary> GetChampionSummaryAsync(ChampionId championId, GameVersion version,
            Language? language = null)
        {
            var data = await GetBaseDataAsync<ChampionFullDataDto, ChampionFullData>(version, language, championId)
                .ConfigureAwait(false);
            return data.Champions[championId];
            // I don't want to cache summaries, I get full champion instead and expose it as summary.
        }

        /// <inheritdoc/>
        public async Task<ChampionSummary> FetchChampionSummaryAsync(ChampionId championId, GameVersion? version = null,
            Language? language = null)
        {
            var data = await FetchBaseDataAsync<ChampionFullDataDto, ChampionFullData>(version, language, championId)
                .ConfigureAwait(false);
            return data.Champions[championId]; // Same as above.
        }

        /// <inheritdoc/>
        public async ValueTask<Champion> GetChampionAsync(ChampionId championId, GameVersion version,
            Language? language = null)
        {
            var data = await GetBaseDataAsync<ChampionFullDataDto, ChampionFullData>(version, language, championId)
                .ConfigureAwait(false);
            return data.Champions[championId];
        }

        /// <inheritdoc/>
        public async Task<Champion> FetchChampionAsync(ChampionId championId, GameVersion? version = null,
            Language? language = null)
        {
            var data = await FetchBaseDataAsync<ChampionFullDataDto, ChampionFullData>(version, language, championId)
                .ConfigureAwait(false);
            return data.Champions[championId];
        }

        /// <inheritdoc/>
        public async ValueTask<IReadOnlyCollection<SummonerSpell>> GetSummonerSpellsAsync(GameVersion version,
            Language? language = null)
        {
            var data = await GetBaseDataAsync<SummonerSpellDataDto, SummonerSpellData>(version, language)
                .ConfigureAwait(false);
            return data.SummonerSpells.Values;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<SummonerSpell>> FetchSummonerSpellsAsync(GameVersion? version = null,
            Language? language = null)
        {
            var data = await FetchBaseDataAsync<SummonerSpellDataDto, SummonerSpellData>(version, language)
                .ConfigureAwait(false);
            return data.SummonerSpells.Values;
        }

        /// <inheritdoc/>
        public async ValueTask<IReadOnlyCollection<ProfileIcon>> GetProfileIconsAsync(GameVersion version,
            Language? language = null)
        {
            var data = await GetBaseDataAsync<ProfileIconDataDto, ProfileIconData>(version, language)
                .ConfigureAwait(false);
            return data.ProfileIcons.Values;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<ProfileIcon>> FetchProfileIconsAsync(GameVersion? version = null,
            Language? language = null)
        {
            var data = await FetchBaseDataAsync<ProfileIconDataDto, ProfileIconData>(version, language)
                .ConfigureAwait(false);
            return data.ProfileIcons.Values;
        }

        /// <inheritdoc/>
        public async ValueTask<IReadOnlyCollection<Map>> GetMapsAsync(GameVersion version, Language? language = null)
        {
            var data = await GetBaseDataAsync<MapDataDto, MapData>(version, language).ConfigureAwait(false);
            return data.Maps.Values;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<Map>> FetchMapsAsync(GameVersion? version = null,
            Language? language = null)
        {
            var data = await FetchBaseDataAsync<MapDataDto, MapData>(version, language).ConfigureAwait(false);
            return data.Maps.Values;
        }

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

                var latestVersion = await InternalFetchLatestVersionAsync().ConfigureAwait(false);

                language ??= DefaultLanguage;
                version ??= latestVersion;

                if (version > latestVersion)
                    throw new ArgumentOutOfRangeException(nameof(version),
                        "The provided version is higher than the latest Data Dragon version.");

                var data = await MakeRequestAsync<IReadOnlyList<RuneDto>, IReadOnlyList<Rune>>(
                    $"{Cdn}/{version}/data/{language.Value.GetCode()}/runesReforged.json",
                    dtos => dtos.Select(x => new Rune(x)).ToImmutableArray()).ConfigureAwait(false);

                if (_options.CacheMode != CacheMode.None)
                    Cache<IReadOnlyList<Rune>>.Instance[version, language.Value] = data;

                return data;
            }
            catch (Exception e)
            {
                throw new DataNotFoundException(
                    $"We couldn't fetch data for the version: {version}. Read the inner exception for more details.",
                    e,
                    $"{_client.BaseAddress}/{Cdn}/{version}/data/{language}/runesReforged.json",
                    version!,
                    language!.Value);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        /// <inheritdoc/>
        public async ValueTask<IReadOnlyCollection<MissionAsset>> GetMissionAssetsAsync(GameVersion version,
            Language? language = null)
        {
            var data = await GetBaseDataAsync<MissionAssetDataDto, MissionAssetData>(version, language)
                .ConfigureAwait(false);
            return data.MissionAssets.Values;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<MissionAsset>> FetchMissionAssetsAsync(GameVersion? version = null,
            Language? language = null)
        {
            var data = await FetchBaseDataAsync<MissionAssetDataDto, MissionAssetData>(version, language)
                .ConfigureAwait(false);
            return data.MissionAssets.Values;
        }

        public void Dispose()
        {
            lock (_lock)
            {
                if (_isDisposed)
                    return;

                _client.CancelPendingRequests();
                _client.Dispose();
                _isDisposed = true;

                GC.SuppressFinalize(this);
            }
        }
    }
}