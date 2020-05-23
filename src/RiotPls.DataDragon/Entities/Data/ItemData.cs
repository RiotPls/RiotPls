using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    internal class ItemData : BaseData
    {
        public ReadOnlyDictionary<int, Item> Items { get; }

        public IReadOnlyList<ItemGroup> Groups { get; }

        public IReadOnlyList<ItemTree> Tree { get; }

        internal ItemData(ItemDataDto dto) : base(dto)
        {
            Items = new ReadOnlyDictionary<int, Item>(
                dto.Items.ToDictionary(
                    x => int.Parse(x.Key),
                    y => new Item(y.Value)));;
            Groups = dto.Groups.Select(x => new ItemGroup(x)).ToImmutableArray();
            Tree = dto.Tree.Select(x => new ItemTree(x)).ToImmutableArray();
        }
    }
}