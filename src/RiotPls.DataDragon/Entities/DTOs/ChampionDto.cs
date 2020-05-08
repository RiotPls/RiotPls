using System.Collections.Generic;
using System.Text.Json.Serialization;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class ChampionDto : ChampionBaseDto
    {
        [JsonPropertyName("lore")]
        public string Lore { get; set; }

        [JsonPropertyName("skins")]
        public IReadOnlyCollection<ChampionSkinDto> Skins { get; set; }

        [JsonPropertyName("allytips")]
        public IReadOnlyCollection<string> AllyTips { get; set; }

        [JsonPropertyName("enemytips")]
        public IReadOnlyCollection<string> EnemyTips { get; set; }

        [JsonPropertyName("spells")]
        public IReadOnlyCollection<SpellDto> Spells { get; set; }

        [JsonPropertyName("passive")]
        public SpellBaseDto Passive { get; set; }

        [JsonPropertyName("recommended")]
        public IReadOnlyCollection<RecommendationDto> Recommendations { get; set; }
    }
}