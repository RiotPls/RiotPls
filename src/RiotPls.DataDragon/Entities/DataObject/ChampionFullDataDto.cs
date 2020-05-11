using System.Collections.Generic;
using System.Text.Json.Serialization;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class ChampionFullDataDto : BaseDataDto
    {
        [JsonPropertyName("data")]
        public IReadOnlyDictionary<string, ChampionDto> Champions { get; set; }

        //[JsonPropertyName("keys")]
        [JsonIgnore] // no need.
        public IReadOnlyDictionary<int, string> ChampionKeys { get; set; }
    }
}