using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using RiotPls.DataDragon.Converters;
using RiotPls.DataDragon.Entities;
using RiotPls.DataDragon.Extensions;

namespace RiotPls.DataDragon
{
    public class DataDragonClient : IDisposable
    {
        public const string Host = "https://ddragon.leagueoflegends.com";
        public const string Api = "/api";
        public const string Cdn = "/cdn";
        public const string DefaultLanguage = "en_US";

        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions();
        
        public DataDragonClient(HttpClient client = null, JsonSerializerOptions jsonSerializerOptions = null)
        {
            _client = client ?? new HttpClient();
            _client.BaseAddress = new Uri(Host);
            _jsonSerializerOptions.Converters.Add(new GameVersionConverter());
        }

        /// <summary>
        ///     Returns a list of every available version of Data Dragon.
        /// </summary>
        public async Task<IReadOnlyCollection<GameVersion>> GetVersionsAsync()
        {
            var request = await _client.GetStreamAsync($"{Api}/versions.json");
            return await JsonSerializer.DeserializeAsync<IReadOnlyCollection<GameVersion>>(request, _jsonSerializerOptions);
        }
        
        /// <summary>
        ///     Returns a list of every available language for the latest version of Data Dragon, expressed as UTF-8 culture codes. (i.e. en_US)
        /// </summary>
        public async Task<IReadOnlyCollection<string>> GetLanguagesAsync()
        {
            var request = await _client.GetStreamAsync($"{Cdn}/languages.json");
            return await JsonSerializer.DeserializeAsync<IReadOnlyCollection<string>>(request);
        }

        /// <summary>
        ///     Returns a <see cref="ChampionData"/> containing base information about every champion on the game.
        /// </summary>
        /// <param name="version">The version of Data Dragon to use.</param>
        /// <param name="language">The language in which the data must be returned. Defaults to English (United States).</param>
        public async Task<ChampionData> GetChampionsAsync(string version, string language = DefaultLanguage)
        {
            var request = await _client.GetStreamAsync($"{Cdn}/{version}/data/{language}/champion.json");
            return await JsonSerializer.DeserializeAsync<ChampionData>(request);
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}