using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

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
        
        public async Task<IReadOnlyCollection<string>> GetVersionsAsync()
        {
            var request = await _client.GetStreamAsync($"{Host}{Api}/versions.json");
            return await JsonSerializer.DeserializeAsync<IReadOnlyCollection<string>>(request);
        }
    }
}