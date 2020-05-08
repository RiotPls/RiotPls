using System.Collections.Generic;
using System.Collections.Immutable;

namespace RiotPls.DataDragon.Entities
{
    // todo: find a better way to handle when its optional, so null.
    public sealed class LevelTip
    {
        public IReadOnlyList<string> Labels { get; }

        public IReadOnlyList<string> Effects { get; }

        internal LevelTip(LevelTipDto dto)
        {
            Labels = dto?.Labels.ToImmutableArray() ?? ImmutableArray<string>.Empty;
            Effects = dto?.Effects.ToImmutableArray() ?? ImmutableArray<string>.Empty;
        }
    }
}