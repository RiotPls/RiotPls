﻿using System.Text.Json.Serialization;
using RiotPls.DataDragon.Enums;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class ChampionSummaryDto
    {
        // todo: make it optional?
        [JsonPropertyName("version")]
        public GameVersion Version { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("key")] // This has to be a string
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
        public StaticImageDto Image { get; set; }

        [JsonPropertyName("tags")]
        public ChampionType Tags { get; set; }

        [JsonPropertyName("stats")]
        public ChampionStatsDto Stats { get; set; }
    }
}