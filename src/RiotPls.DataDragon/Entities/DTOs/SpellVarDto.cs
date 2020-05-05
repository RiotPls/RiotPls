using System.Text.Json.Serialization;
using RiotPls.DataDragon.Converters;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    // todo: check later if there are missing properties
    internal class SpellVarDto
    {
        [JsonPropertyName("link")]
        public string Link { get; set; }
        
        [JsonPropertyName("coeff")] 
        [JsonConverter(typeof(DoubleArrayJsonConverter))]
        public double[] Coeff { get; set; }
        
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}