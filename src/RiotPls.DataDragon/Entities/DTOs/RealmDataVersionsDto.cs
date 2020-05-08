using System.Text.Json.Serialization;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal partial class RealmDataVersionsDto
    {
        [JsonPropertyName("item")]
        public GameVersion ItemVersion { get; set; }

        [JsonPropertyName("rune")]
        public GameVersion RuneVersion { get; set; }

        [JsonPropertyName("mastery")]
        public GameVersion MasteryVersion { get; set; }

        [JsonPropertyName("summoner")]
        public GameVersion SummonerVersion { get; set; }

        [JsonPropertyName("champion")]
        public GameVersion ChampionVersion { get; set; }

        [JsonPropertyName("profileicon")]
        public GameVersion ProfileIconVersion { get; set; }

        [JsonPropertyName("map")]
        public GameVersion MapVersion { get; set; }

        [JsonPropertyName("language")]
        public GameVersion LanguageVersion { get; set; }

        [JsonPropertyName("sticker")]
        public GameVersion StickerVersion { get; set; }
    }
}
