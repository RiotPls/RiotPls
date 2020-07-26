using System.Text.Json.Serialization;

#nullable disable
namespace RiotPls.Entities.DataObject
{
    internal sealed class SummonerDto
    {
        [JsonPropertyName("accountId")]
        public string AccountId { get; set; }
        
        [JsonPropertyName("profileIconId")]
        public int ProfileIconId { get; set; }
        
        [JsonPropertyName("revisionDate")]
        public long RevisionDate { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        [JsonPropertyName("puuid")]
        public string Puuid { get; set; }
        
        [JsonPropertyName("summonerLevel")]
        public long Level { get; set; }
    }
}