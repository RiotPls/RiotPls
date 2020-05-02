using System.Collections.Generic;
using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    public sealed class Block
    {
        public string Type { get; }
        
        public bool RecMath { get; }
        
        public bool RecSteps { get; }
        
        public int MinSummonerLevel { get; }
        
        public int MaxSummonerLevel { get; }
        
        public string ShowIfSummonerSpell { get; }
        
        public string HideIfSummonerSpell { get; }
        
        public string AppendAfterSection { get; }
        
        public IReadOnlyCollection<string> VisibleWithAllOf { get; }
        
        public IReadOnlyCollection<string> HiddenWithAllOf { get; }
        
        public IReadOnlyCollection<ItemBlock> Items { get; }

        internal Block(BlockDto dto)
        {
            Type = dto.Type;
            RecMath = dto.RecMath;
            RecSteps = dto.RecSteps;
            MinSummonerLevel = dto.MinSummonerLevel;
            MaxSummonerLevel = dto.MaxSummonerLevel;
            ShowIfSummonerSpell = dto.ShowIfSummonerSpell;
            HideIfSummonerSpell = dto.HideIfSummonerSpell;
            AppendAfterSection = dto.AppendAfterSection;
            VisibleWithAllOf = dto.VisibleWithAllOf;
            HiddenWithAllOf = dto.HiddenWithAllOf;
            Items = dto.Items.Select(x => new ItemBlock(x)).ToList().AsReadOnly();
        }
    }
}