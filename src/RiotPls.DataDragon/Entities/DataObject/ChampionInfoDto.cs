using System.Text.Json.Serialization;

namespace RiotPls.DataDragon.Entities
{
    internal class ChampionInfoDto
    {
        [JsonPropertyName("attack")]
        public int Attack { get; set; }

        [JsonPropertyName("defense")]
        public int Defense { get; set; }

        [JsonPropertyName("magic")]
        public int Magic { get; set; }

        [JsonPropertyName("difficulty")]
        public int Difficulty { get; set; }
    }
}