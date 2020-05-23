using System.Collections.Generic;
using System.Collections.Immutable;

namespace RiotPls.DataDragon.Entities
{
    public class ItemGroup
    {
        /// <summary>
        ///     Header of the group.
        /// </summary>
        public string Header { get; }

        /// <summary>
        ///     Tags of the group.
        /// </summary>
        public IReadOnlyList<string> Tags { get; }

        internal ItemGroup(ItemGroupDto dto)
        {
            Header = dto.Header;
            Tags = dto.Tags?.ToImmutableArray() ?? ImmutableArray<string>.Empty;
        }
    }
}