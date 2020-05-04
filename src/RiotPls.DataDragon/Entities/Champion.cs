using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    public sealed class Champion : ChampionBase
    {
        /// <summary>
        ///     Lore of the champion.
        /// </summary>
        public string Lore { get; }
        
        /// <summary>
        ///     Skins of the champion.
        /// </summary>
        public ReadOnlyCollection<ChampionSkin> Skins { get; }
        
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
        public ReadOnlyCollection<Spell> Spells { get; }
        
        /// <summary>
        ///     Passive spell of the champion.
        /// </summary>
        public SpellBase PassiveSpell { get; }
        
        /// <summary>
        ///     Recommendations by their game mode.
        /// </summary>
        public IReadOnlyCollection<Recommendation> Recommendations { get; }

        internal Champion(DataDragonClient client, ChampionDto dto) : base(client, dto)
        {
            Lore = dto.Lore;
            Skins = dto.Skins.Select(x => new ChampionSkin(client, x)).ToList().AsReadOnly();
            AllyTips = dto.AllyTips;
            EnemyTips = dto.EnemyTips;
            Spells = dto.Spells.Select(x => new Spell(client, x)).ToList().AsReadOnly();
            PassiveSpell = new SpellBase(client, dto.Passive);
            Recommendations = dto.Recommendations.Select(x => new Recommendation(client, x)).ToList().AsReadOnly();
        }
    }
}