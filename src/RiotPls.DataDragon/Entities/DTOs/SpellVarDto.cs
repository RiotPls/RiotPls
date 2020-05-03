using System.Text.Json.Serialization;

#nullable disable
namespace RiotPls.DataDragon.Entities
{
    // todo: check later if there are missing properties
    internal class SpellVarDto
    {
        [JsonPropertyName("link")]
        public string Link { get; set; }
        
        // todo: create a converter for double that can be double[]
        // [JsonPropertyName("coeff")] 
        [JsonIgnore]
        public string Coeff { get; set; }
        
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}