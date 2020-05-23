using System.Text.Json.Serialization;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class RuneSlotDto
    {
        [JsonPropertyName("runes")]
        public RuneItemDto[] Runes { get; set; }
    }
}