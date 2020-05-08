using System.Collections.ObjectModel;
using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    public class ChampionFullData : BaseData
    {
        /// <summary>
        ///     A dictionary of champion objects, keyed by their unique identifiers.
        /// </summary>
        public ReadOnlyDictionary<int, Champion> Champions { get; }

        internal ChampionFullData(ChampionFullDataDto dto) : base(dto)
        {
            Champions = new ReadOnlyDictionary<int, Champion>(
                dto.Champions.ToDictionary(
                    x => int.Parse(x.Value.Key),
                    y => new Champion(y.Value)));
        }
    }
}