using System;
using RiotPls.DataDragon.Enums;

namespace RiotPls.DataDragon.Entities
{
    public sealed class SpellVar
    {
        public string Link { get; }
        
        // todo: should stay a string *for now* because it can be either double or sometimes double[]... 
        public double[] Coeff { get; }
        
        public SpellKey Key { get; }

        internal SpellVar(SpellVarDto dto)
        {
            Link = dto.Link;
            Coeff = dto.Coeff;
            Key = Enum.Parse<SpellKey>(dto.Key, true);
        }
    }
}