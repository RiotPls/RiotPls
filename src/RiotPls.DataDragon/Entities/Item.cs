using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    public class Item
    {
        /// <summary>
        ///     Name of the item.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Detailed description of the item.
        /// </summary>
        public string Description { get; }

        public string Colloq { get; }

        /// <summary>
        ///     Breve description of the item.
        /// </summary>
        public string Plaintext { get; }

        /// <summary>
        ///      Item ids this item can be used for.
        /// </summary>
        public IReadOnlyList<int> Into { get; }

        /// <summary>
        ///     Image information for this item.
        /// </summary>
        public ImageInformation Image { get; }

        /// <summary>
        ///     Gold needed or given with this item.
        /// </summary>
        public ItemGold Gold { get; }

        /// <summary>
        ///     Tag of the item.
        /// </summary>
        public IReadOnlyList<string> Tags { get; }

        /// <summary>
        ///     Maps this item is available in.
        /// </summary>
        public IReadOnlyDictionary<string, bool> Maps { get; }

        /// <summary>
        ///     Stats given by the item.
        /// </summary>
        public IReadOnlyDictionary<string, double> Stats { get; }

        /// <summary>
        ///     Whether the item is available or not.
        /// </summary>
        public bool? InStore { get; }

        /// <summary>
        ///     Item ids this item comes from.
        /// </summary>
        public IReadOnlyList<int> From { get; }

        /// <summary>
        ///     Effect of the item.
        /// </summary>
        public ItemEffect? Effect { get; }

        public long? Depth { get; }

        /// <summary>
        ///     Amount of stack the item can have.
        /// </summary>
        public long? Stacks { get; }

        /// <summary>
        ///     Whether the item is consumed or not.
        /// </summary>
        public bool? Consumed { get; }

        /// <summary>
        ///     Whether the item must be hidden from other players.
        /// </summary>
        public bool? HideFromAll { get; }

        /// <summary>
        ///     Whether the item must be consumed when full.
        /// </summary>
        public bool? ConsumeOnFull { get; }

        public long? SpecialRecipe { get; }

        /// <summary>
        ///     Champions required for this item.
        /// </summary>
        public string RequiredChampion { get; }

        /// <summary>
        ///     Whether this item requires an ally.
        /// </summary>
        public string RequiredAlly { get; }

        internal Item(ItemDto dto)
        {
            Name = dto.Name;
            Description = dto.Description;
            Colloq = dto.Colloq;
            Plaintext = dto.Plaintext;
            Into = dto.Into?.Where(x => x is object).Select(int.Parse).ToImmutableArray() ?? ImmutableArray<int>.Empty;
            Image = new ImageInformation(dto.Image);
            Gold = new ItemGold(dto.Gold);
            Tags = dto.Tags.ToImmutableArray();
            Maps = new ConcurrentDictionary<string, bool>(
                dto.Maps.ToDictionary(
                    x => x.Key,
                    y => y.Value
                ));
            Stats = new ConcurrentDictionary<string, double>(
                dto.Stats.ToDictionary(
                    x => x.Key,
                    y => y.Value
                ));
            InStore = dto.InStore;
            From = dto.From?.Where(x => x is object).Select(int.Parse).ToImmutableArray() ?? ImmutableArray<int>.Empty;
            
            if (dto.Effect is object)
            {
                Effect = new ItemEffect(dto.Effect);
            }

            Depth = dto.Depth;
            Stacks = dto.Stacks;
            Consumed = dto.Consumed;
            HideFromAll = dto.HideFromAll;
            ConsumeOnFull = dto.ConsumeOnFull;
            SpecialRecipe = dto.SpecialRecipe;
            RequiredChampion = dto.RequiredChampion;
            RequiredAlly = dto.RequiredAlly;
        }
    }
}