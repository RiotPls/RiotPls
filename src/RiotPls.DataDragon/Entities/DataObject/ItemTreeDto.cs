using System.Text.Json.Serialization;
#nullable disable
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