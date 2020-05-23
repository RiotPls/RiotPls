using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    public class Item
    {
        public string Name { get; }

        public string Description { get; }

        public string Colloq { get; }

        public string Plaintext { get; }

        public IReadOnlyList<int> Into { get; }

        public ImageInformation Image { get; }

        public ItemGold Gold { get; }

        public IReadOnlyList<string> Tags { get; }

        public IReadOnlyDictionary<string, bool> Maps { get; }

        public IReadOnlyDictionary<string, double> Stats { get; }

        public bool? InStore { get; }

        public IReadOnlyList<int> From { get; }

        public ItemEffect? Effect { get; }

        public long? Depth { get; }

        public long? Stacks { get; }

        public bool? Consumed { get; }

        public bool? HideFromAll { get; }

        public bool? ConsumeOnFull { get; }

        public long? SpecialRecipe { get; }

        public string RequiredChampion { get; }

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