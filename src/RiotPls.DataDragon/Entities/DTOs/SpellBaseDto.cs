using System.Text.Json.Serialization;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class SpellBaseDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        // todo: add Description that compute numbers.
        // (example: '<physicalDamage>physical damage</physicalDamage>' into '50'
        [JsonPropertyName("description")]
        public string DescriptionRaw { get; set; }
        
        [JsonPropertyName("image")]
        public StaticImageDto Image { get; set; }
    }
}