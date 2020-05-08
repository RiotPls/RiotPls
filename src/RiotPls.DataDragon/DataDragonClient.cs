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
using RiotPls.DataDragon.Helpers;

namespace RiotPls.DataDragon
{
    public sealed class DataDragonClient : IDataDragonClient
    {
        public const string Host = "https://ddragon.leagueoflegends.com";
        public const string Api = "/api";
        public const string Cdn = "/cdn";
        public const string Realm = "/realms/na.json";

        internal static IDataDragonClient Instance { get; } = new DataDragonClient();

        public Language DefaultLanguage { get; }
        public RealmVersion RealmVersion { get; private set; }

        private readonly DataDragonClientOptions _options;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly SemaphoreSlim _semaphore;
        private readonly object _lock;
        private bool _isDisposed;

        internal DataDragonClient() : this(null)
        {
        }

        internal DataDragonClient(DataDragonClientOptions? options)
        {
            _options = options ?? new DataDragonClientOptions();
            DefaultLanguage = _options.DefaultLanguage;
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
            RealmVersion = null!; // we should be aware of this on internal code
        }

        public static Task<IDataDragonClient> CreateAsync()
            => CreateAsync(new DataDragonClientOptions());

        public static async Task<IDataDragonClient> CreateAsync(DataDragonClientOptions options)
        {
            if (options is null)
                throw new ArgumentNullException(nameof(options));

            var client = new DataDragonClient(options);

            await client.InternalUpdateRealmVersionAsync().ConfigureAwait(false);
            return client;
        }

        public async Task UpdateRealmVersionAsync()
        {
            ThrowIfDisposed();

            try
            {
                await _semaphore.WaitAsync().ConfigureAwait(false);
                await InternalUpdateRealmVersionAsync().ConfigureAwait(false);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task InternalUpdateRealmVersionAsync()
        {
            RealmVersion = await MakeRequestAsync<RealmVersionDto, RealmVersion>(Realm, dto => new RealmVersion(dto)).ConfigureAwait(false);
        }


        public ValueTask<IReadOnlyList<GameVersion>> GetVersionsAsync()
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return ValueTaskHelper.Create(
                    this,
                    _options.Versions.Contains(RealmVersion.DataDragonVersion),
                    client => client._options.Versions[client.RealmVersion.DataDragonVersion],
                    async client =>
                    {
                        try
                        {
                            await client._semaphore.WaitAsync().ConfigureAwait(false);

                            var data = await client.MakeRequestAsync<IReadOnlyList<GameVersion>>(
                                $"{Api}/versions.json").ConfigureAwait(false);
                            var latestVersion = data[0];

                            if (latestVersion > client.RealmVersion.DataDragonVersion)
                                await client.InternalUpdateRealmVersionAsync().ConfigureAwait(false);

                            client._options.Versions[latestVersion] = data;
                            return data;
                        }
                        finally
                        {
                            client._semaphore.Release();
                        }
                    });
            }
        }

        public async Task<GameVersion> FetchLatestVersionAsync()
        {
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

        private async Task<GameVersion> InternalFetchLatestVersionAsync()
        {
            var data = await MakeRequestAsync<string[]>($"{Api}/versions.json").ConfigureAwait(false);
            var latestVersion = GameVersion.Parse(data[0]);

            if (latestVersion > RealmVersion.DataDragonVersion)
                await InternalUpdateRealmVersionAsync().ConfigureAwait(false);

            _options.Versions[latestVersion] = Array.ConvertAll(data, GameVersion.Parse);
            return latestVersion;
        }

        /// <summary>
        ///    Returns a list of every available language for the latest version
        ///    of Data Dragon, expressed as UTF-8 culture codes. (i.e. en_US)
        /// </summary>
        public ValueTask<IReadOnlyList<Language>> GetLanguagesAsync()
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return ValueTaskHelper.Create(
                    this,
                    client => client._options.Languages.Contains(RealmVersion.DataVersion.LanguageVersion),
                    client => client._options.Languages[RealmVersion.DataVersion.LanguageVersion],
                    async client =>
                    {
                        try
                        {
                            await _semaphore.WaitAsync().ConfigureAwait(false);

                            var data = await client.MakeRequestAsync<IReadOnlyList<Language>>(
                                $"{Cdn}/languages.json").ConfigureAwait(false);
                            var latestVersion = await client.FetchLatestVersionAsync().ConfigureAwait(false);

                            client._options.Languages[latestVersion] = data;
                            return data;
                        }
                        finally
                        {
                            _semaphore.Release();
                        }
                    });
            }
        }

        /// <summary>
        ///    Returns a <see cref="ChampionBaseData"/> containing base information
        ///    about every champion on the game.
        /// </summary>
        public ValueTask<ChampionBaseData> GetPartialChampionAsync()
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return GetPartialChampionsAsync(RealmVersion.DataVersion.ChampionVersion, DefaultLanguage);
            }
        }

        /// <summary>
        ///    Returns a <see cref="ChampionBaseData"/> containing base information
        ///    about every champion on the game.
        /// </summary>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        public ValueTask<ChampionBaseData> GetPartialChampionsAsync(GameVersion version)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return GetPartialChampionsAsync(version, DefaultLanguage);
            }
        }

        /// <summary>
        ///    Returns a <see cref="ChampionBaseData"/> containing base information
        ///    about every champion on the game.
        /// </summary>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///    The language in which the data must be returned. Defaults to English (United States).
        /// </param>
        public ValueTask<ChampionBaseData> GetPartialChampionsAsync(GameVersion version, Language language)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return ValueTaskHelper.Create(
                    (Client: this, Version: version, language.GetCode()),
                    _options.PartialChampions.Contains(version),
                    state => state.Client._options.PartialChampions[state.Version],
                    async state =>
                    {
                        var (client, version, language) = state;
                        var data = await client.MakeRequestAsync<ChampionBaseDataDto, ChampionBaseData>(
                            $"{Cdn}/{version}/data/{language}/champion.json",
                            dto => new ChampionBaseData(dto)).ConfigureAwait(false);

                        client._options.PartialChampions[version] = data;
                        return data;
                    });
            }
        }

        public ValueTask<ChampionFullData> GetChampionsAsync()
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return GetChampionsAsync(RealmVersion.DataVersion.ChampionVersion, DefaultLanguage);
            }
        }

        /// <summary>
        ///    Returns a <see cref="ChampionFullData"/> containing full information
        ///    about every champion on the game.
        /// </summary>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        public ValueTask<ChampionFullData> GetChampionsAsync(GameVersion version)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return GetChampionsAsync(version, DefaultLanguage);
            }
        }

        /// <summary>
        ///    Returns a <see cref="ChampionFullData"/> containing full information
        ///    about every champion on the game.
        /// </summary>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///    The language in which the data must be returned. Defaults to English (United States).
        /// </param>
        public ValueTask<ChampionFullData> GetChampionsAsync(GameVersion version, Language language)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return ValueTaskHelper.Create(
                    (Client: this, Version: version, language.GetCode()),
                    _options.FullChampions.Contains(version),
                    state => state.Client._options.FullChampions[state.Version],
                    async state =>
                    {
                        var (client, version, language) = state;
                        var data = await client.MakeRequestAsync<ChampionFullDataDto, ChampionFullData>(
                            $"{Cdn}/{version}/data/{language}/championFull.json",
                            dto => new ChampionFullData(dto)).ConfigureAwait(false);

                        client._options.FullChampions[version] = data;
                        return data;
                    });
            }
        }

        /// <summary>
        ///    Returns a <see cref="ChampionData"/> containing full information
        ///    about a specific champion on the game.
        /// </summary>
        /// <param name="championName">
        ///    Name of the champion to query.
        /// </param>
        /// <param name="version">
        ///   The version of Data Dragon to use.
        /// </param>
        public ValueTask<ChampionData> GetChampionAsync(ChampionId championName, GameVersion version)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return GetChampionAsync(championName, version, DefaultLanguage);
            }
        }

        /// <summary>
        ///    Returns a <see cref="ChampionData"/> containing full information
        ///    about a specific champion on the game.
        /// </summary>
        /// <param name="championName">
        ///    Name of the champion to query.
        /// </param>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///    The language in which the data must be returned. Defaults to <see cref="Language.AmericanEnglish"/>.
        /// </param>
        public ValueTask<ChampionData> GetChampionAsync(ChampionId championName, GameVersion version, Language language)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return ValueTaskHelper.Create(
                    (Client: this, ChampionName: championName, Version: version, language.GetCode()),
                    _options.Champions.TryGetValue(championName, out var cache) && cache.Contains(version),
                    state => state.Client._options.Champions[state.ChampionName].GetData(state.Version)!,
                    async state =>
                    {
                        var (client, championName, version, language) = state;

                        try
                        {
                            await client._semaphore.WaitAsync().ConfigureAwait(false);

                            var data = await client.MakeRequestAsync<ChampionDataDto, ChampionData>(
                                $"{Cdn}/{version}/data/{language}/champion/{championName}.json",
                                dto => new ChampionData(dto)).ConfigureAwait(false);

                            client._options._champions.AddOrUpdate(
                                championName,
                                (name, state) => 
                                {
                                    var (version, data) = state;
                                    var cache = CacheControl<ChampionData>.Instance;

                                    cache.TryAddData(version, data);
                                    return cache;
                                },
                                (name, cache, state) => 
                                {
                                    var (version, data) = state;

                                    cache.TryAddData(version, data);
                                    return cache;
                                },
                                (Version: version, Data: data));
                            return data;
                        }
                        finally
                        {
                            client._semaphore.Release();
                        }
                    });
            }
        }

        /// <summary>
        ///    Returns a <see cref="SummonerSpellData"/> containing full information
        ///    about the whole game's summoner spells
        /// </summary>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        public ValueTask<SummonerSpellData> GetSummonerSpellsAsync(GameVersion version)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return GetSummonerSpellsAsync(version, DefaultLanguage);
            }
        }

        /// <summary>
        ///    Returns a <see cref="SummonerSpellData"/> containing full information
        ///    about the whole game's summoner spells
        /// </summary>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///    The language in which the data must be returned. Defaults to English (United States).
        /// </param>
        public ValueTask<SummonerSpellData> GetSummonerSpellsAsync(GameVersion version, Language language)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return ValueTaskHelper.Create(
                    (Client: this, Version: version, language.GetCode()),
                    _options.SummonerSpells.Contains(version),
                    state => state.Client._options.SummonerSpells[state.Version],
                    async state =>
                    {
                        var (client, version, language) = state;
                        try
                        {
                            await client._semaphore.WaitAsync().ConfigureAwait(false);

                            var data = await client.MakeRequestAsync<SummonerSpellDataDto, SummonerSpellData>(
                                $"{Cdn}/{version}/data/{language}/summoner.json",
                                dto => new SummonerSpellData(dto)).ConfigureAwait(false);

                            client._options.SummonerSpells[version] = data;
                            return data;
                        }
                        finally
                        {
                            client._semaphore.Release();
                        }
                    });
            }
        }

        /// <summary>
        ///    Returns a <see cref="ProfileIconData"/> containing full information
        ///    about the whole profile icons.
        /// </summary>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        public ValueTask<ProfileIconData> GetProfileIconsAsync(GameVersion version)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return GetProfileIconsAsync(version, DefaultLanguage);
            }
        }

        /// <summary>
        ///    Returns a <see cref="SummonerSpellData"/> containing full information
        ///    about the whole game's summoner spells
        /// </summary>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///    The language in which the data must be returned. Defaults to English (United States).
        /// </param>
        public ValueTask<ProfileIconData> GetProfileIconsAsync(GameVersion version, Language language)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return ValueTaskHelper.Create(
                    (Client: this, Version: version, language.GetCode()),
                    _options.SummonerSpells.Contains(version),
                    state => state.Client._options.ProfileIcons[state.Version],
                    async state =>
                    {
                        var (client, version, language) = state;

                        try
                        {
                            await client._semaphore.WaitAsync().ConfigureAwait(false);

                            var data = await client.MakeRequestAsync<ProfileIconDataDto, ProfileIconData>(
                                $"{Cdn}/{version}/data/{language}/profileicon.json",
                                dto => new ProfileIconData(dto)).ConfigureAwait(false);

                            client._options.ProfileIcons[version] = data;
                            return data;
                        }
                        finally
                        {
                            client._semaphore.Release();
                        }
                    });
            }
        }
        
        /// <summary>
        ///    Returns a <see cref="MapData"/> containing full information
        ///    about the whole maps.
        /// </summary>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        public ValueTask<MapData> GetMapsAsync(GameVersion version)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return GetMapsAsync(version, DefaultLanguage);
            }
        }
        
        /// <summary>
        ///    Returns a <see cref="MapData"/> containing full information
        ///    about the whole maps.
        /// </summary>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///    The language in which the data must be returned. Defaults to English (United States).
        /// </param>
        public ValueTask<MapData> GetMapsAsync(GameVersion version, Language language)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return ValueTaskHelper.Create(
                    (Client: this, Version: version, language.GetCode()),
                    _options.Maps.Contains(version),
                    state => state.Client._options.Maps[state.Version],
                    async state =>
                    {
                        var (client, version, language) = state;

                        try
                        {
                            await client._semaphore.WaitAsync().ConfigureAwait(false);

                            var data = await client.MakeRequestAsync<MapDataDto, MapData>(
                                $"{Cdn}/{version}/data/{language}/map.json",
                                dto => new MapData(dto)).ConfigureAwait(false);

                            client._options.Maps[version] = data;
                            return data;
                        }
                        finally
                        {
                            client._semaphore.Release();
                        }
                    });
            }
        }
        
        /// <summary>
        ///    Returns a set of <see cref="Rune"/> containing full information
        ///    about the different runes and their slots.
        /// </summary>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        public ValueTask<IReadOnlyList<Rune>> GetRunesAsync(GameVersion version)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return GetRunesAsync(version, DefaultLanguage);
            }
        }
        
        /// <summary>
        ///    Returns a set of <see cref="Rune"/> containing full information
        ///    about the different runes and their slots.
        /// </summary>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///    The language in which the data must be returned. Defaults to English (United States).
        /// </param>
        public ValueTask<IReadOnlyList<Rune>> GetRunesAsync(GameVersion version, Language language)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return ValueTaskHelper.Create(
                    (Client: this, Version: version, language.GetCode()),
                    _options.Runes.Contains(version),
                    state => state.Client._options.Runes[state.Version],
                    async state =>
                    {
                        var (client, version, language) = state;

                        try
                        {
                            await client._semaphore.WaitAsync().ConfigureAwait(false);

                            var data = await client.MakeRequestAsync<IReadOnlyList<RuneDto>, IReadOnlyList<Rune>>(
                                $"{Cdn}/{version}/data/{language}/runesReforged.json",
                                dtos => dtos.Select(x => new Rune(x)).ToImmutableArray()).ConfigureAwait(false);

                            client._options.Runes[version] = data;
                            return data;
                        }
                        finally
                        {
                            client._semaphore.Release();
                        }
                    });
            }
        }
        
        /// <summary>
        ///    Returns a set of <see cref="MissionAssetData"/> containing full information
        ///    about the different mission assets.
        /// </summary>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        public ValueTask<MissionAssetData> GetMissionAssetsAsync(GameVersion version)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return GetMissionAssetsAsync(version, DefaultLanguage);
            }
        }
        
        /// <summary>
        ///    Returns a set of <see cref="MissionAssetData"/> containing full information
        ///    about the different mission assets.
        /// </summary>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///    The language in which the data must be returned. Defaults to English (United States).
        /// </param>
        public ValueTask<MissionAssetData> GetMissionAssetsAsync(GameVersion version, Language language)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return ValueTaskHelper.Create(
                    (Client: this, Version: version, language.GetCode()),
                    _options.MissionAssets.Contains(version),
                    state => state.Client._options.MissionAssets[state.Version],
                    async state =>
                    {
                        var (client, version, language) = state;

                        try
                        {
                            await client._semaphore.WaitAsync().ConfigureAwait(false);
                            var data = await client.MakeRequestAsync<MissionAssetDataDto, MissionAssetData>(
                                $"{Cdn}/{version}/data/{language}/mission-assets.json",
                                dto => new MissionAssetData(dto)).ConfigureAwait(false);

                            client._options.MissionAssets[version] = data;
                            return data;
                        }
                        finally
                        {
                            client._semaphore.Release();
                        }
                    });
            }
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
            }
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
    }
}