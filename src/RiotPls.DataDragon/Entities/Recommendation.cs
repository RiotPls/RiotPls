using System.Collections.ObjectModel;
using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    public sealed class Recommendation
    {
        /// <summary>
        ///     Name of the champion
        /// </summary>
        public string ChampionName { get; }
        
        public string Title { get; }
        
        /// <summary>
        ///     Map on which this recommendation applies.
        /// </summary>
        public string Map { get; }
        
        /// <summary>
        ///     Game mode for this recommendation.
        /// </summary>
        public string Mode { get; }
        
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
        
        // todo: see what that is
        public object CustomPanel { get; }
        
        public ReadOnlyCollection<Block> Blocks { get; }

        internal Recommendation(RecommendationDto dto)
        {
            ChampionName = dto.ChampionName;
            Title = dto.Title;
            Map = dto.Map;
            Mode = dto.Mode;
            Type = dto.Type;
            CustomTag = dto.CustomTag;
            SortRank = dto.SortRank;
            HasExtensionPage = dto.HasExtensionPage;
            UseObviousCheckMark = dto.UseObviousCheckMark;
            CustomPanel = dto.CustomPanel;
            Blocks = dto.Blocks.Select(x => new Block(x)).ToList().AsReadOnly();
        }
    }
}