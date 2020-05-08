using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using RiotPls.DataDragon.Enums;

namespace RiotPls.DataDragon.Entities
{
    public sealed class SpellVar
    {
        public string Link { get; }

        public IReadOnlyList<double> Coeff { get; }

        public SpellKey Key { get; }

        internal SpellVar(SpellVarDto dto)
        {
            Link = dto.Link;
            Coeff = dto.Coeff.ToImmutableArray();
            Key = Enum.Parse<SpellKey>(dto.Key, true);
        }
    }
}