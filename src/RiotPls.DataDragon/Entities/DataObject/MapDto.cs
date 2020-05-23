using System.Text.Json.Serialization;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class MapDto
    {
        [JsonPropertyName("MapName")]
        public string Name { get; set; }

        [JsonPropertyName("MapId")]
        public string Id { get; set; }

        [JsonPropertyName("image")]
        public StaticImageDto Image { get; set; }
    }
}