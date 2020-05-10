using System.Collections.Generic;
using System.Text.Json.Serialization;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class MapDataDto : BaseDataDto
    {
        [JsonPropertyName("data")]
        public IReadOnlyDictionary<string, MapDto> Maps { get; set; }
    }
}