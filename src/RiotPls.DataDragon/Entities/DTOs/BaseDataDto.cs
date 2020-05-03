using System.Text.Json.Serialization;
using RiotPls.DataDragon.Converters;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class BaseDataDto
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; }
        
        [JsonPropertyName("version")]
        [JsonConverter(typeof(GameVersionJsonConverter))]
        public GameVersion Version { get; set; }
    }
}