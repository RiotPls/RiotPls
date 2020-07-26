using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable
namespace RiotPls.Entities.DataObject
{
    internal sealed class LeagueListDto
    {
        [JsonPropertyName("leagueId")]
        public string LeagueId { get; set; }
        
        [JsonPropertyName("entries")]
        public List<LeagueItemDto> Entries { get; set; }
        
        [JsonPropertyName("tier")]
        public string Tier { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("queue")]
        public string Queue { get; set; }
    }
}