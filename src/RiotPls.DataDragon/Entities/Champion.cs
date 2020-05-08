using System;
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
        public ChampionSkin[] Skins { get; }

        /// <summary>
        ///     Ally tips of the champion.
        /// </summary>
        public string[] AllyTips { get; }

        /// <summary>
        ///     Enemy tips of the champion.
        /// </summary>
        public string[] EnemyTips { get; }

        /// <summary>
        ///     Spells of the champion.
        /// </summary>
        public Spell[] Spells { get; }

        /// <summary>
        ///     Passive spell of the champion.
        /// </summary>
        public SpellBase PassiveSpell { get; }

        /// <summary>
        ///     Recommendations by their game mode.
        /// </summary>
        public Recommendation[] Recommendations { get; }

        internal Champion(ChampionDto dto) : base(dto)
        {
            Lore = dto.Lore;
            Skins = Array.ConvertAll(dto.Skins, dto => new ChampionSkin(dto));
            AllyTips = dto.AllyTips;
            EnemyTips = dto.EnemyTips;
            Spells = Array.ConvertAll(dto.Spells, dto => new Spell(dto));
            PassiveSpell = new SpellBase(dto.Passive);
            Recommendations = Array.ConvertAll(dto.Recommendations, dto => new Recommendation(dto));
        }
    }
}