using System.Collections.Generic;
using System.Text.Json.Serialization;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class BlockDto
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        
        [JsonPropertyName("recMath")]
        public bool RecMath { get; set; }
        
        [JsonPropertyName("recSteps")]
        public bool RecSteps { get; set; }
        
        [JsonPropertyName("minSummonerLevel")]
        public int MinSummonerLevel { get; set; }
        
        [JsonPropertyName("maxSummonerLevel")]
        public int MaxSummonerLevel { get; set; }
        
        [JsonPropertyName("showIfSummonerSpell")]
        public string ShowIfSummonerSpell { get; set; }
        
        [JsonPropertyName("hideIfSummonerSpell")]
        public string HideIfSummonerSpell { get; set; }
        
        [JsonPropertyName("appendAfterSection")]
        public string AppendAfterSection { get; set; }
        
        [JsonPropertyName("visibleWithAllof")]
        public IReadOnlyCollection<string> VisibleWithAllOf { get; set; }
        
        [JsonPropertyName("hiddenWithAllOf")]
        public IReadOnlyCollection<string> HiddenWithAllOf { get; set; }
        
        [JsonPropertyName("items")]
        public IReadOnlyCollection<ItemBlockDto> Items { get; set; }
    }
}