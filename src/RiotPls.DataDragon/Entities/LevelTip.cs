using System.Collections.Generic;
using System.Collections.Immutable;

namespace RiotPls.DataDragon.Entities
{
    public sealed class LevelTip
    {
        /// <summary>
        ///     Labels of the level tip.
        /// </summary>
        public IReadOnlyList<string> Labels { get; }

        /// <summary>
        ///     Effects of the level tip.
        /// </summary>
        public IReadOnlyList<string> Effects { get; }

        internal LevelTip(LevelTipDto dto)
        {
            Labels = dto?.Labels.ToImmutableArray() ?? ImmutableArray<string>.Empty;
            Effects = dto?.Effects.ToImmutableArray() ?? ImmutableArray<string>.Empty;
        }
    }
}