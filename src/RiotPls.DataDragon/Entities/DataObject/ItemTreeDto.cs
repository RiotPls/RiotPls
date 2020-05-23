using System.Text.Json.Serialization;

namespace RiotPls.DataDragon.Entities
{
    public class ItemTreeDto
    {
        [JsonPropertyName("header")]
        public string Header { get; set; }

        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }
    }
}