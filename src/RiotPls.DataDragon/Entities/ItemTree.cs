using System.Collections.Generic;
using System.Collections.Immutable;

namespace RiotPls.DataDragon.Entities
{
    public class ItemTree
    {
        public string Header { get; }

        public IReadOnlyList<string> Tags { get; }

        internal ItemTree(ItemTreeDto dto)
        {
            Header = dto.Header;
            Tags = dto.Tags.ToImmutableArray();
        }
    }
}