using System.Text.Json.Serialization;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class ChampionDto : ChampionBaseDto
    {
        [JsonPropertyName("lore")]
        public string Lore { get; set; }

        [JsonPropertyName("skins")]
        public ChampionSkinDto[] Skins { get; set; }

        [JsonPropertyName("allytips")]
        public string[] AllyTips { get; set; }

        [JsonPropertyName("enemytips")]
        public string[] EnemyTips { get; set; }

        [JsonPropertyName("spells")]
        public SpellDto[] Spells { get; set; }

        [JsonPropertyName("passive")]
        public SpellBaseDto Passive { get; set; }

        [JsonPropertyName("recommended")]
        public RecommendationDto[] Recommendations { get; set; }
    }
}