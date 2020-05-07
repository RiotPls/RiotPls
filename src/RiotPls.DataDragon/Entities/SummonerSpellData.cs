using System.Collections.ObjectModel;
using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    public sealed class SummonerSpellData : BaseData
    {
        /// <summary>
        ///     A dictionary of summoner spells, keyed by their unique identifier.
        /// </summary>
        public ReadOnlyDictionary<int, SummonerSpell> SummonerSpells { get; }
        
        internal SummonerSpellData(SummonerSpellDataDto dto) : base(dto)
        {
            SummonerSpells = new ReadOnlyDictionary<int, SummonerSpell>(
                dto.SummonerSpells.ToDictionary(
                    x => int.Parse(x.Value.Key),
                    y => new SummonerSpell(y.Value)));
        }
    }
}