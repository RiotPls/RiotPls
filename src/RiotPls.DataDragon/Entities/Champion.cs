using System;
using System.Collections.Generic;
using System.Collections.Immutable;

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
        public IReadOnlyList<ChampionSkin> Skins { get; }

        /// <summary>
        ///     Ally tips of the champion.
        /// </summary>
        public IReadOnlyList<string> AllyTips { get; }

        /// <summary>
        ///     Enemy tips of the champion.
        /// </summary>
        public IReadOnlyList<string> EnemyTips { get; }

        /// <summary>
        ///     Spells of the champion.
        /// </summary>
        public IReadOnlyList<Spell> Spells { get; }

        /// <summary>
        ///     Passive spell of the champion.
        /// </summary>
        public SpellBase PassiveSpell { get; }

        /// <summary>
        ///     Recommendations by their game mode.
        /// </summary>
        public IReadOnlyList<Recommendation> Recommendations { get; }

        internal Champion(ChampionDto dto) : base(dto)
        {
            Lore = dto.Lore;
            Skins = Array.ConvertAll(dto.Skins, x => new ChampionSkin(x)).ToImmutableArray();
            AllyTips = dto.AllyTips.ToImmutableArray();
            EnemyTips = dto.EnemyTips.ToImmutableArray();
            Spells = Array.ConvertAll(dto.Spells, x => new Spell(x)).ToImmutableArray();
            PassiveSpell = new SpellBase(dto.Passive);
            Recommendations = Array.ConvertAll(dto.Recommendations, x => new Recommendation(x)).ToImmutableArray();
        }
    }
}