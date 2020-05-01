using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace RiotPls.DataDragon.Entities
{
    internal class RecommendationDto
    {
        [JsonPropertyName("champion")]
        public string ChampionName { get; set; }
        
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("map")]
        public string Map { get; set; }
        
        [JsonPropertyName("mode")]
        public string Mode { get; set; }
        
        [JsonPropertyName("type")]
        public string Type { get; set; }
        
        [JsonPropertyName("customTag")]
        public string CustomTag { get; set; }
        
        [JsonPropertyName("sortRank")]
        public int SortRank { get; set; }
        
        [JsonPropertyName("extensionPage")]
        public bool HasExtensionPage { get; set; }
        
        [JsonPropertyName("useObviousCheckmark")]
        public bool UseObviousCheckMark { get; set; }
        
        [JsonPropertyName("customPanel")]
        public object CustomPanel { get; set; }
        
        [JsonPropertyName("blocks")]
        public ReadOnlyCollection<BlockDto> Blocks { get; set; }
    }
}