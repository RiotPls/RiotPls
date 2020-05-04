using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using RiotPls.DataDragon.Converters;
using RiotPls.DataDragon.Entities;
using RiotPls.DataDragon.Extensions;
using RiotPls.DataDragon.Helpers;

namespace RiotPls.DataDragon
{
    public class DataDragonClient : IDisposable
    {
        public const string Host = "https://ddragon.leagueoflegends.com";
        public const string Api = "/api";
        public const string Cdn = "/cdn";
        public readonly GameLanguage DefaultLanguage;

        private readonly DataDragonClientOptions _options;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly object _lock;
        private volatile bool _isDisposed;

        public DataDragonClient() : this(null)
        {
        }
        
        public DataDragonClient(DataDragonClientOptions? options)
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
            _jsonSerializerOptions.Converters.Add(GameVersionJsonConverter.Instance);
            _jsonSerializerOptions.Converters.Add(GameLanguageJsonConverter.Instance);
            
            _lock = new object();
        }

        /// <summary>
        ///     Returns a list of every available version of Data Dragon.
        /// </summary>
        public ValueTask<IReadOnlyList<GameVersion>> GetVersionsAsync()
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return ValueTaskHelper.Create(
                      !_options.Versions.IsExpired,
                      this,
                      @this => @this._options.Versions.Data!,
                      async @this =>
                      {
                          var data = await @this.MakeRequestAsync<IReadOnlyList<GameVersion>>(
                              $"{Api}/versions.json").ConfigureAwait(false);

                          @this._options.Versions.Data = data;
                          return data;
                      });
            }
        }

        /// <summary>
        ///     Returns a list of every available language for the latest version
        ///     of Data Dragon, expressed as UTF-8 culture codes. (i.e. en_US)
        /// </summary>
        public ValueTask<IReadOnlyList<GameLanguage>> GetLanguagesAsync()
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return ValueTaskHelper.Create(
                      !_options.Languages.IsExpired,
                      this,
                      @this => @this._options.Languages.Data!,
                      async @this =>
                      {
                          var data = await @this.MakeRequestAsync<IReadOnlyList<GameLanguage>>(
                              $"{Cdn}/languages.json").ConfigureAwait(false);

                          @this._options.Languages.Data = data;
                          return data;
                      });
            }
        }

        /// <summary>
        ///     Returns a <see cref="ChampionBaseData"/> containing base information
        ///     about every champion on the game.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
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
        ///     Returns a <see cref="ChampionBaseData"/> containing base information
        ///     about every champion on the game.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language in which the data must be returned. Defaults to English (United States).
        /// </param>
        public ValueTask<ChampionBaseData> GetPartialChampionsAsync(GameVersion version, GameLanguage language)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return ValueTaskHelper.Create(
                      !_options.PartialChampions.IsExpired,
                      (Client: this, Version: version, Language: language),
                      state => state.Client._options.PartialChampions.Data!,
                      async state =>
                      {
                          var data = await state.Client.MakeRequestAsync<ChampionBaseDataDto, ChampionBaseData>(
                              $"{Cdn}/{state.Version}/data/{state.Language}/champion.json", 
                              dto => new ChampionBaseData(state.Client, dto)).ConfigureAwait(false);

                          state.Client._options.PartialChampions.Data = data;
                          return data;
                      });
            }
        }
        
        /// <summary>
        ///     Returns a <see cref="ChampionFullData"/> containing full information
        ///     about every champion on the game.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
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
        ///     Returns a <see cref="ChampionFullData"/> containing full information
        ///     about every champion on the game.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language in which the data must be returned. Defaults to English (United States).
        /// </param>
        public ValueTask<ChampionFullData> GetChampionsAsync(GameVersion version, GameLanguage language)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return ValueTaskHelper.Create(
                    !_options.PartialChampions.IsExpired,
                    (Client: this, Version: version, Language: language),
                    state => state.Client._options.FullChampions.Data!,
                    async state =>
                    {
                        var data = await state.Client.MakeRequestAsync<ChampionFullDataDto, ChampionFullData>(
                            $"{Cdn}/{state.Version}/data/{state.Language}/championFull.json", 
                            dto => new ChampionFullData(state.Client, dto)).ConfigureAwait(false);

                        state.Client._options.FullChampions.Data = data;
                        return data;
                    });
            }
        }

        /// <summary>
        ///     Returns a <see cref="ChampionData"/> containing full information
        ///     about a specific champion on the game.
        /// </summary>
        /// <param name="championName">
        ///     Name of the champion to query.
        /// </param>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        public ValueTask<ChampionData> GetChampionAsync(string championName, GameVersion version)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return GetChampionAsync(championName, version, DefaultLanguage);
            }
        }

        /// <summary>
        ///     Returns a <see cref="ChampionData"/> containing full information
        ///     about a specific champion on the game.
        /// </summary>
        /// <param name="championName">
        ///     Name of the champion to query.
        /// </param>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///    The language in which the data must be returned. Defaults to English (United States).
        /// </param>
        public ValueTask<ChampionData> GetChampionAsync(string championName, GameVersion version, GameLanguage language)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return ValueTaskHelper.Create(
                    _options.Champions.TryGetValue(championName.CapitalizeFirstLetter(), out var cache) && !cache.IsExpired,
                    (Client: this, ChampionName: championName, Version: version, Language: language),
                    state => state.Client._options.Champions[state.ChampionName].Data!,
                    async state =>
                    {
                        var data = await state.Client.MakeRequestAsync<ChampionDataDto, ChampionData>(
                            $"{Cdn}/{state.Version}/data/{state.Language}/champion/{state.ChampionName}.json", 
                            dto => new ChampionData(state.Client, dto)).ConfigureAwait(false);
                        
                        state.Client._options._champions.AddOrUpdate(state.ChampionName,
                            (_, s) => CacheControl<ChampionData>.TimedCache(s.Client._options.ChampionFullCacheDuration),
                            (_, __, s) =>
                                CacheControl<ChampionData>.TimedCache(s.Client._options.ChampionFullCacheDuration),
                            state);

                        return data;
                    });
            }
        }

        /// <summary>
        ///     Returns a <see cref="SummonerSpellData"/> containing full information
        ///     about the whole game's summoner spells
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
        ///     Returns a <see cref="SummonerSpellData"/> containing full information
        ///     about the whole game's summoner spells
        /// </summary>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///    The language in which the data must be returned. Defaults to English (United States).
        /// </param>
        public ValueTask<SummonerSpellData> GetSummonerSpellsAsync(GameVersion version, GameLanguage language)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return ValueTaskHelper.Create(
                    !_options.SummonerSpells.IsExpired,
                    (Client: this, Version: version, Language: language),
                    state => state.Client._options.SummonerSpells.Data!,
                    async state =>
                    {
                        var data = await state.Client.MakeRequestAsync<SummonerSpellDataDto, SummonerSpellData>(
                            $"{Cdn}/{state.Version}/data/{state.Language}/summoner.json", 
                            dto => new SummonerSpellData(state.Client, dto)).ConfigureAwait(false);

                        state.Client._options.SummonerSpells.Data = data;

                        return data;
                    });
            }
        }

        private async Task<TEntity> MakeRequestAsync<TDto, TEntity>(string url, Func<TDto, TEntity> ctor)
            => ctor(await MakeRequestAsync<TDto>(url).ConfigureAwait(false));

        private async Task<TEntity> MakeRequestAsync<TEntity>(string url)
        {
            var request = await _client.GetStreamAsync(url).ConfigureAwait(false);
            return await JsonSerializer.DeserializeAsync<TEntity>(
                request, _jsonSerializerOptions).ConfigureAwait(false);
        }

        private void ThrowIfDisposed()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().Name);
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
            }
        }
    }
}