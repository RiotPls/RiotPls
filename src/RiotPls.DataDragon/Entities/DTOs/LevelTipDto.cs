using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RiotPls.DataDragon.Entities
{
    internal class LevelTipDto
    {
        [JsonPropertyName("label")]
        public IReadOnlyCollection<string> Labels { get; set; }
        
        [JsonPropertyName("effect")]
        public IReadOnlyCollection<string> Effects { get; set; }
    }
}