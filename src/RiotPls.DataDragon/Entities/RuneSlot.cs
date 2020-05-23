using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    public class RuneSlot
    {
        /// <summary>
        ///     Different rune items for that rune slot.
        /// </summary>
        public IReadOnlyList<RuneItem> Runes { get; }

        internal RuneSlot(RuneSlotDto dto)
        {
            Runes = dto.Runes.Select(x => new RuneItem(x)).ToImmutableArray();
        }
    }
}