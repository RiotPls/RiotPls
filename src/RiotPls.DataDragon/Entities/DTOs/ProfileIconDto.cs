using System.Text.Json.Serialization;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class ProfileIconDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("image")]
        public StaticImageDto Image { get; set; }
    }
}