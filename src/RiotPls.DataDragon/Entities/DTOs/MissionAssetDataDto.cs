using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RiotPls.DataDragon.Entities
{
    internal class MissionAssetDataDto : BaseDataDto
    {
        [JsonPropertyName("data")]
        public IReadOnlyDictionary<string, MissionAssetDto> MissionAssets { get; set; }
    }
}