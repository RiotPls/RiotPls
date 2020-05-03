using System.Collections.Generic;
using System.Text.Json.Serialization;
#nullable disable
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
        public IReadOnlyCollection<BlockDto> Blocks { get; set; }
        
        [JsonPropertyName("requiredPerk")]
        public string RequiredPerk { get; set; }
        
        [JsonPropertyName("extenOrnnPage")]
        public bool ExtennOrnnPage { get; set; }
    }
}