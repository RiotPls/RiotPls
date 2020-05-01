using System.Collections.Generic;
using System.Text.Json.Serialization;
using RiotPls.DataDragon.Converters;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class ChampionBaseDto
    {
        [JsonPropertyName("version")]
        [JsonConverter(typeof(GameVersionConverter))]
        public GameVersion Version { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("blurb")]
        public string Blurb { get; set; }

        [JsonPropertyName("partype")]
        public string Partype { get; set; }

        [JsonPropertyName("info")]
        public ChampionInfoDto Info { get; set; }

        [JsonPropertyName("image")]
        public ChampionImageDto Image { get; set; }
        
        [JsonPropertyName("tags")]
        public IReadOnlyCollection<string> Tags { get; set; }

        [JsonPropertyName("stats")]
        public ChampionStatsDto Stats { get; set; } 
    }
}