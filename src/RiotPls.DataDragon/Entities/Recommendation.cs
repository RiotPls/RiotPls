using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using RiotPls.DataDragon.Enums;

namespace RiotPls.DataDragon.Entities
{
    public sealed class Recommendation
    {
        /// <summary>
        ///     Name of the champion
        /// </summary>
        public string ChampionName { get; }

        /// <summary>
        ///     Title of the champion
        /// </summary>
        public string Title { get; }

        /// <summary>
        ///     Map on which this recommendation applies.
        /// </summary>
        public RecommendationMap Map { get; }

        /// <summary>
        ///     Game mode for this recommendation.
        /// </summary>
        public RecommendationMode Mode { get; }

        /// <summary>
        ///     Type of recommendation.
        /// </summary>
        public string Type { get; }

        /// <summary>
        ///     Tag given for this recommendation.
        /// </summary>
        public string CustomTag { get; }

        /// <summary>
        ///     Sort rank of this recommendation.
        /// </summary>
        public int SortRank { get; }

        /// <summary>
        ///     Whether this recommendation has extension pages. 
        /// </summary>
        public bool HasExtensionPage { get; }

        /// <summary>
        ///     Whether to use obvious check mark for this recommendation.
        /// </summary>
        public bool UseObviousCheckMark { get; }

        // todo: see what that is, seems to always be null
        public object? CustomPanel { get; }

        public string RequiredPerk { get; }

        public bool ExtennOrnnPage { get; }

        public IReadOnlyList<Block> Blocks { get; }

        internal Recommendation(RecommendationDto dto)
        {
            ChampionName = dto.ChampionName;
            Title = dto.Title;
            Map = Enum.Parse<RecommendationMap>(dto.Map, true);
            Mode = Enum.Parse<RecommendationMode>(dto.Mode.Replace("_", ""), true);
            Type = dto.Type;
            CustomTag = dto.CustomTag;
            SortRank = dto.SortRank;
            HasExtensionPage = dto.HasExtensionPage;
            UseObviousCheckMark = dto.UseObviousCheckMark;
            CustomPanel = dto.CustomPanel;
            Blocks = Array.ConvertAll(dto.Blocks, x => new Block(x)).ToImmutableArray();
            RequiredPerk = dto.RequiredPerk;
            ExtennOrnnPage = dto.ExtennOrnnPage;
        }
    }
}