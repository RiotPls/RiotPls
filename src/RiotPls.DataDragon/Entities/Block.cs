using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using RiotPls.DataDragon.Enums;

namespace RiotPls.DataDragon.Entities
{
    public sealed class Block
    {
        public string Type { get; }

        public bool RecMath { get; }

        public bool RecSteps { get; }

        public int MinSummonerLevel { get; }

        public int MaxSummonerLevel { get; }

        public BlockSummonerSpell ShowIfSummonerSpell { get; }

        public BlockSummonerSpell HideIfSummonerSpell { get; }

        public string AppendAfterSection { get; }

        public IReadOnlyList<string> VisibleWithAllOf { get; }

        public IReadOnlyList<string> HiddenWithAllOf { get; }

        public IReadOnlyList<ItemBlock> Items { get; }

        internal Block(BlockDto dto)
        {
            Type = dto.Type;
            RecMath = dto.RecMath;
            RecSteps = dto.RecSteps;
            MinSummonerLevel = dto.MinSummonerLevel;
            MaxSummonerLevel = dto.MaxSummonerLevel;
            AppendAfterSection = dto.AppendAfterSection;
            VisibleWithAllOf = dto.VisibleWithAllOf?.ToImmutableArray() ?? ImmutableArray<string>.Empty;
            HiddenWithAllOf = dto.HiddenWithAllOf?.ToImmutableArray() ?? ImmutableArray<string>.Empty;
            Items = Array.ConvertAll(dto.Items, x => new ItemBlock(x)).ToImmutableArray();

            if (!string.IsNullOrWhiteSpace(dto.ShowIfSummonerSpell))
                ShowIfSummonerSpell =
                    Enum.Parse<BlockSummonerSpell>(dto.ShowIfSummonerSpell.Replace("_", string.Empty), true);

            if (!string.IsNullOrWhiteSpace(dto.HideIfSummonerSpell))
                HideIfSummonerSpell =
                    Enum.Parse<BlockSummonerSpell>(dto.HideIfSummonerSpell.Replace("_", string.Empty), true);
        }
    }
}