using System.Collections.Generic;
using System.Text.Json.Serialization;
using RiotPls.DataDragon.Converters;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class ChampionDataDto
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; }
        
        [JsonPropertyName("version")]
        [JsonConverter(typeof(GameVersionConverter))]
        public GameVersion Version { get; set; }

        [JsonPropertyName("data")]
        public IReadOnlyDictionary<string, ChampionBaseDto> Champions { get; set; }
    }
}