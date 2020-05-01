using System.Text.Json.Serialization;

namespace RiotPls.DataDragon.Entities
{
    internal class ItemBlockDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("count")]
        public int Count { get; set; }
        
        [JsonPropertyName("hideCount")]
        public bool HideCount { get; set; }
    }
}