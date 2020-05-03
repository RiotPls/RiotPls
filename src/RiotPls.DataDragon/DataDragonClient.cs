using System;
using System.Collections.Generic;
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
        public const string DefaultLanguage = "en_US";

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
            _client = new HttpClient 
            {
                BaseAddress = new Uri(Host)
            };
            _jsonSerializerOptions = new JsonSerializerOptions();
            _jsonSerializerOptions.Converters.Add(GameVersionJsonConverter.Instance);
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
                          var request = await @this._client.GetStreamAsync($"{Api}/versions.json").ConfigureAwait(false);
                          var data = await JsonSerializer.DeserializeAsync<IReadOnlyList<GameVersion>>(
                              request, @this._jsonSerializerOptions).ConfigureAwait(false);

                          @this._options.Versions.Data = data;
                          return data;
                      });
            }
        }

        /// <summary>
        ///     Returns a list of every available language for the latest version
        ///     of Data Dragon, expressed as UTF-8 culture codes. (i.e. en_US)
        /// </summary>
        public ValueTask<IReadOnlyCollection<string>> GetLanguagesAsync()
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
                          var request = await @this._client.GetStreamAsync($"{Cdn}/languages.json").ConfigureAwait(false);
                          var data = await JsonSerializer.DeserializeAsync<IReadOnlyCollection<string>>(
                              request, @this._jsonSerializerOptions).ConfigureAwait(false);

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
        public ValueTask<ChampionBaseData> GetChampionsAsync(GameVersion version)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return GetChampionsAsync(version, DefaultLanguage);
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
        public ValueTask<ChampionBaseData> GetChampionsAsync(GameVersion version, string language)
        {
            lock (_lock)
            {
                ThrowIfDisposed();
                return ValueTaskHelper.Create(
                      !_options.BaseChampions.IsExpired,
                      (Client: this, Version: version, Language: language),
                      state => state.Client._options.BaseChampions.Data!,
                      async state =>
                      {
                          var request = await state.Client._client.GetStreamAsync(
                              $"{Cdn}/{state.Version}/data/{state.Language}/champion.json").ConfigureAwait(false);
                          var dto = await JsonSerializer.DeserializeAsync<ChampionBaseDataDto>(
                              request, state.Client._jsonSerializerOptions).ConfigureAwait(false);
                          var data = new ChampionBaseData(dto);

                          state.Client._options.BaseChampions.Data = data;
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
        public ValueTask<ChampionData> GetChampionAsync(string championName, GameVersion version, string language)
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
                        var request = await state.Client._client.GetStreamAsync(
                                $"{Cdn}/{state.Version}/data/{state.Language}/champion/{state.ChampionName}.json")
                            .ConfigureAwait(false);
                        var dto = await JsonSerializer.DeserializeAsync<ChampionDataDto>(
                            request, state.Client._jsonSerializerOptions).ConfigureAwait(false);
                        var data = new ChampionData(dto);

                        state.Client._options._champions.AddOrUpdate(state.ChampionName,
                            (_, s) => CacheControl<ChampionData>.TimedCache(s.Client._options.ChampionFullCacheDuration),
                            (_, __, s) =>
                                CacheControl<ChampionData>.TimedCache(s.Client._options.ChampionFullCacheDuration),
                            state);
                        return data;
                    });
            }
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