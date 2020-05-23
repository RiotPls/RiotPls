using System.Collections.Generic;
using System.Text.Json.Serialization;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class ItemDataDto : BaseDataDto
    {
        [JsonPropertyName("data")]
        public IReadOnlyDictionary<string, ItemDto> Items { get; set; }

        [JsonPropertyName("groups")]
        public ItemGroupDto[] Groups { get; set; }

        [JsonPropertyName("tree")]
        public ItemTreeDto[] Tree { get; set; }
    }
}