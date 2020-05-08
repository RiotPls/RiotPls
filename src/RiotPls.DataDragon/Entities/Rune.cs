using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    public class Rune
    {
        /// <summary>
        ///     Id of the rune.
        /// </summary>
        public int Id { get; }

        /// <summary>
        ///     Key of the rune.
        /// </summary>
        public string Key { get; }
        
        /// <summary>
        ///     Icon path of the rune.
        /// </summary>
        public string Icon { get; }
        
        /// <summary>
        ///     Name of the rune. 
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        ///     Different rune slots for that rune.
        /// </summary>
        public IReadOnlyList<IReadOnlyList<RuneSlot>> Slots { get; }

        internal Rune(RuneDto dto)
        {
            Id = dto.Id;
            Key = dto.Key;
            Icon = dto.Icon;
            Name = dto.Name;
            Slots = dto.Slots.Select(
                x => (IReadOnlyList<RuneSlot>)x.Select(y => new RuneSlot(y)).ToImmutableArray()).ToImmutableArray();
        }
    }
}