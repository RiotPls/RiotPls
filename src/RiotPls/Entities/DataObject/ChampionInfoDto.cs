using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable
namespace RiotPls.Entities.DataObject
{
    internal sealed class ChampionInfoDto
    {
        [JsonPropertyName("maxNewPlayerLevel")]
        public int MaxNewPlayerLevel { get; set; }
        
        [JsonPropertyName("freeChampionIdsForNewPlayers")]
        public List<int> FreeChampionIdsForNewPlayers { get; set; }
        
        [JsonPropertyName("freeChampionIds")]
        public List<int> FreeChampionIds { get; set; }
    }
}