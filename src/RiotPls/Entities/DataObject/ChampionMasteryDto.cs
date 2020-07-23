using System.Text.Json.Serialization;

#nullable disable
namespace RiotPls.Entities.DataObject
{
    internal sealed class ChampionMasteryDto
    {
        [JsonPropertyName("championPointsUntilNextLevel")]
        public long PointsUntilNextLevel { get; set; }
        
        [JsonPropertyName("chestGranted")]
        public bool ChestGranted { get; set; }
        
        [JsonPropertyName("championId")]
        public long ChampionId { get; set; }
        
        [JsonPropertyName("lastPlayTime")]
        public long LastPlayTime { get; set; }
        
        [JsonPropertyName("championLevel")]
        public int Level { get; set; }
        
        [JsonPropertyName("summonerId")]
        public string SummonerId { get; set; }
        
        [JsonPropertyName("championPoints")]
        public int Points { get; set; }
        
        [JsonPropertyName("championPointsSinceLastLevel")]
        public long PointsSinceLastLevel { get; set; }
        
        [JsonPropertyName("tokensEarned")]
        public int TokensEarned { get; set; }
    }
}