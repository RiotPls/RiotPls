using System.Text.Json.Serialization;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class ChampionSkinDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("num")]
        public int Num { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("chromas")]
        public bool HasChromas { get; set; }
    }
}