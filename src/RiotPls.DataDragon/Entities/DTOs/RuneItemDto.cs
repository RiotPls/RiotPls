using System.Text.Json.Serialization;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    public class RuneItemDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("key")]
        public string Key { get; set; }
        
        [JsonPropertyName("icon")]
        public string Icon { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("shortDesc")]
        public string ShortDescription { get; set; }
        
        [JsonPropertyName("longDesc")]
        public string LongDescription { get; set; }
    }
}