using System;
using System.Collections.Generic;

namespace RiotPls.DataDragon.Entities
{
    // todo: find a better way to handle when its optional, so null.
    public sealed class LevelTip
    {
        public IReadOnlyCollection<string> Labels { get; }

        public IReadOnlyCollection<string> Effects { get; }

        internal LevelTip(LevelTipDto dto)
        {
            Labels = dto?.Labels ?? Array.Empty<string>();
            Effects = dto?.Effects ?? Array.Empty<string>();
        }
    }
}