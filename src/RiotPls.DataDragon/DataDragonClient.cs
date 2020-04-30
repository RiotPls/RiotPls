using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using RiotPls.DataDragon.Converters;
using RiotPls.DataDragon.Entities;

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
        
        public DataDragonClient(DataDragonClientOptions clientOptions = null)
        {
            _options = clientOptions ?? new DataDragonClientOptions();
            _client = new HttpClient { BaseAddress = new Uri(Host) };
            
            _jsonSerializerOptions = new JsonSerializerOptions();
            _jsonSerializerOptions.Converters.Add(new GameVersionConverter());
        }

        /// <summary>
        ///     Returns a list of every available version of Data Dragon.
        /// </summary>
        public async Task<IReadOnlyCollection<GameVersion>> GetVersionsAsync()
        {
            if (!_options.Versions.IsExpired) return _options.Versions.Data;
            
            var request = await _client.GetStreamAsync($"{Api}/versions.json");
            var data = await JsonSerializer.DeserializeAsync<IReadOnlyCollection<GameVersion>>(
                request, _jsonSerializerOptions);
            _options.Versions.Data = data;
            return _options.Versions.Data;
        }
        
        /// <summary>
        ///     Returns a list of every available language for the latest version
        ///     of Data Dragon, expressed as UTF-8 culture codes. (i.e. en_US)
        /// </summary>
        public async Task<IReadOnlyCollection<string>> GetLanguagesAsync()
        {
            if (!_options.Languages.IsExpired) return _options.Languages.Data;
            
            var request = await _client.GetStreamAsync($"{Cdn}/languages.json");
            var data = await JsonSerializer.DeserializeAsync<IReadOnlyCollection<string>>(
                request, _jsonSerializerOptions);
            _options.Languages.Data = data;
            return _options.Languages.Data;
        }

        /// <summary>
        ///     Returns a <see cref="ChampionData"/> containing base information
        ///     about every champion on the game.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language in which the data must be returned. Defaults to English (United States).
        /// </param>
        public async Task<ChampionData> GetChampionsAsync(
            string version, string language = DefaultLanguage)
        {
            if (!_options.Champions.IsExpired) return _options.Champions.Data;
            
            var request = await _client.GetStreamAsync(
                $"{Cdn}/{version}/data/{language}/champion.json");
            var data = await JsonSerializer.DeserializeAsync<ChampionData>(
                request, _jsonSerializerOptions);
            _options.Champions.Data = data;
            return _options.Champions.Data;
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}