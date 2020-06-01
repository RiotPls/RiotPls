using System.Text.Json.Serialization;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class ItemTreeDto
    {
        [JsonPropertyName("header")]
        public string Header { get; set; }

        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }
    }
}