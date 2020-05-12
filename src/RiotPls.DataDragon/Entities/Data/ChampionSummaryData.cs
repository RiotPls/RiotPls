using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    /// <summary>
    ///     Represents information about League of Legends champions.
    /// </summary>
    internal sealed class ChampionSummaryData : BaseData
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
                    Converter));
        }

        private ChampionSummary Converter(KeyValuePair<string, ChampionSummaryDto> pair)
            => new ChampionSummary(pair.Value, Version);
    }
}