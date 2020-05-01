using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    public class Champion
    {
        /// <summary>
        ///     Lore of the champion.
        /// </summary>
        public string Lore { get; }
        
        /// <summary>
        ///     Skins of the champion.
        /// </summary>
        public IReadOnlyCollection<ChampionSkin> Skins { get; }
        
        /// <summary>
        ///     Ally tips of the champion.
        /// </summary>
        public IReadOnlyCollection<string> AllyTips { get; }
        
        /// <summary>
        ///     Enemy tips of the champion.
        /// </summary>
        public IReadOnlyCollection<string> EnemyTips { get; }
        
        /// <summary>
        ///     Spells of the champion.
        /// </summary>
        public IReadOnlyCollection<Spell> Spells { get; }
        
        /// <summary>
        ///     Passive spell of the champion.
        /// </summary>
        public SpellBase PassiveSpell { get; }
        
        /// <summary>
        ///     Recommendations by their game mode.
        /// </summary>
        public IReadOnlyDictionary<string, Recommendation> Recommended { get; }

        internal Champion(ChampionDto dto)
        {
            // todo: version will be null, so give it in endpoint methods
            Lore = dto.Lore;
            Skins = dto.Skins.Select(x => new ChampionSkin(x)).ToList().AsReadOnly();
            AllyTips = dto.AllyTips;
            EnemyTips = dto.EnemyTips;
            Spells = dto.Spells.Select(x => new Spell(x)).ToList().AsReadOnly();
            PassiveSpell = new SpellBase(dto.Passive);
            Recommended = new ReadOnlyDictionary<string, Recommendation>(
                dto.Recommended.ToDictionary(x => x.Mode, y => new Recommendation(y)));
        }
    }
}