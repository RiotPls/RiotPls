using System.Collections.Generic;
using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    /// <summary>
    ///     Represents a champion's spell.
    /// </summary>
    public sealed class Spell
    {
        /// <summary>
        ///     Id of the spell.
        /// </summary>
        public string Id { get; }

        /// <summary>
        ///     Tool tip for the spell.
        /// </summary>
        public string ToolTip { get; }

        /// <summary>
        ///     Level tip for the spell.
        /// </summary>
        public LevelTip LevelTip { get; }
        
        /// <summary>
        ///     Maximum rank this spell can have.
        /// </summary>
        public int MaxRank { get; }
        
        /// <summary>
        ///     Cooldown per level of the spell.
        /// </summary>
        public IReadOnlyCollection<double> Cooldowns { get; }

        /// <summary>
        ///     Cost per level of the spell.
        /// </summary>
        public IReadOnlyCollection<double> Costs { get; }
        
        // todo: check this object. (always empty)
        public object DataValues { get; }
        
        // todo: check this object.
        public IReadOnlyCollection<IReadOnlyCollection<double>> Effects { get; }
        
        // todo: dunno what it is for.
        public IReadOnlyCollection<SpellVar> Vars { get; }
        
        /// <summary>
        ///     Type of the cost.
        /// </summary>
        public string CostType { get; }
        
        /// <summary>
        ///     Max ammo of the spell.
        /// </summary>
        public int MaxAmmo { get; }
        
        /// <summary>
        ///     Ranges per level of the spell.
        /// </summary>
        public IReadOnlyCollection<int> Ranges { get; }
        
        /// <summary>
        ///     Resources of the spell.
        /// </summary>
        public string Resource { get; }

        internal Spell(SpellDto dto)
        {
            Id = dto.Id;
            ToolTip = dto.ToolTip;
            LevelTip = new LevelTip(dto.LevelTip);
            MaxRank = dto.MaxRank;
            Cooldowns = dto.Cooldowns;
            Costs = dto.Costs;
            DataValues = dto.DataValues;
            Effects = dto.Effects;
            Vars = dto.Vars.Select(x => new SpellVar(x)).ToList().AsReadOnly();
            CostType = dto.CostType;
            MaxAmmo = int.Parse(dto.MaxAmmo);
            Ranges = dto.Ranges;
            Resource = dto.Resource;
        }
    }
}