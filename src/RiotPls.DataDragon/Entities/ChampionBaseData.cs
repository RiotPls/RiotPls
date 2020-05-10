using System.Collections.ObjectModel;
using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    /// <summary>
    ///     Represents information about League of Legends champions.
    /// </summary>
    public sealed class ChampionBaseData : BaseData
    {
        /// <summary>
        ///     A dictionary of champion objects, keyed by their unique identifiers.
        /// </summary>
        public ReadOnlyDictionary<int, ChampionBase> Champions { get; }

        internal ChampionBaseData(ChampionBaseDataDto dto) : base(dto)
        {
            Champions = new ReadOnlyDictionary<int, ChampionBase>(
                dto.Champions.ToDictionary(
                    x => int.Parse(x.Value.Key),
                    y => new ChampionBase(y.Value)));
        }
    }
}