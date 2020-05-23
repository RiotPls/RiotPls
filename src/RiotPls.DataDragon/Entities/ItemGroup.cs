using System.Collections.Generic;
using System.Collections.Immutable;

namespace RiotPls.DataDragon.Entities
{
    public class ItemGroup
    {
        public string Header { get; }

        public IReadOnlyList<string> Tags { get; }

        internal ItemGroup(ItemGroupDto dto)
        {
            Header = dto.Header;
            Tags = dto.Tags?.ToImmutableArray() ?? ImmutableArray<string>.Empty;
        }
    }
}