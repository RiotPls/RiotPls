using System.Text.Json.Serialization;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class SummonerSpellDto : SpellDto
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
        
        [JsonPropertyName("summonerLevel")]
        public int SummonerLevel { get; set; }
    }
}