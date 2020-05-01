using System.Text.Json.Serialization;

namespace RiotPls.DataDragon.Entities
{
    internal class StaticImageDto
    {
        [JsonPropertyName("full")]
        public string Full { get; set; }

        [JsonPropertyName("sprite")]
        public string Sprite { get; set; }

        [JsonPropertyName("group")]
        public string Group { get; set; }
        
        [JsonPropertyName("x")]
        public int X { get; set; }
        
        [JsonPropertyName("y")]
        public int Y { get; set; }
        
        [JsonPropertyName("w")]
        public int W { get; set; }
        
        [JsonPropertyName("h")]
        public int H { get; set; }
    }
}