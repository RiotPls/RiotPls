using System.Collections.Generic;
using System.Text.Json.Serialization;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class LevelTipDto
    {
        [JsonPropertyName("label")]
        public string[] Labels { get; set; }

        [JsonPropertyName("effect")]
        public string[] Effects { get; set; }
    }
}