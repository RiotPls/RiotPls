using System.Collections.ObjectModel;
using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    /// <summary>
    ///     Represents information about League of Legends champions.
    /// </summary>
    public sealed class ChampionSummaryData : BaseData
    {
        /// <summary>
        ///     A dictionary of champion objects, keyed by their unique identifiers.
        /// </summary>
        public ReadOnlyDictionary<int, ChampionSummary> Champions { get; }

        internal ChampionSummaryData(ChampionSummaryDataDto dto) : base(dto)
        {
            Champions = new ReadOnlyDictionary<int, ChampionSummary>(
                dto.Champions.ToDictionary(
                    x => int.Parse(x.Value.Key),
                    y => new ChampionSummary(y.Value)));
        }
    }
}