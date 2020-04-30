using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using RiotPls.DataDragon.Entities;

namespace RiotPls.DataDragon
{
    public class DataDragonClient
    {
        public const string Host = "https://ddragon.leagueoflegends.com";
        public const string Api = "/api";
        public const string Cdn = "/cdn";

        private readonly HttpClient _client;
        
        public DataDragonClient(HttpClient client = null)
        {
            _client = client ?? new HttpClient();
        }

        /// <summary>
        /// Returns a list of every available version of Data Dragon.
        /// </summary>
        public async Task<IReadOnlyCollection<string>> GetVersionsAsync()
        {
            var request = await _client.GetStreamAsync($"{Host}{Api}/versions.json");
            return await JsonSerializer.DeserializeAsync<IReadOnlyCollection<string>>(request);
        }
        
        /// <summary>
        /// Returns a list of every available languages for this version of Data Dragon.
        /// </summary>
        public async Task<IReadOnlyCollection<string>> GetLanguagesAsync()
        {
            var request = await _client.GetStreamAsync($"{Host}{Cdn}/languages.json");
            return await JsonSerializer.DeserializeAsync<IReadOnlyCollection<string>>(request);
        }

        /// <summary>
        /// Returns a <see cref="ChampionData"/> containing base information about every champion on the game.
        /// </summary>
        /// <param name="version">Version of Data Dragon.</param>
        /// <param name="language">Language in which the data must be returned.</param>
        public async Task<ChampionData> GetChampionsAsync(string version, string language = "en_US")
        {
            var request = await _client.GetStreamAsync($"{Host}{Cdn}/{version}/data/{language}/champion.json");
            return await JsonSerializer.DeserializeAsync<ChampionData>(request);
        }
    }
}