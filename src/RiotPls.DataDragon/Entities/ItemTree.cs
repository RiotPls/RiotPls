using System.Collections.Generic;
using System.Collections.Immutable;

namespace RiotPls.DataDragon.Entities
{
    public class ItemTree
    {
        /// <summary>
        ///     Header of the tree.
        /// </summary>
        public string Header { get; }

        /// <summary>
        ///     Tags of the tree.
        /// </summary>
        public IReadOnlyList<string> Tags { get; }

        internal ItemTree(ItemTreeDto dto)
        {
            Header = dto.Header;
            Tags = dto.Tags.ToImmutableArray();
        }
    }
}