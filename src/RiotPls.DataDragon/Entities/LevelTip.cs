using System.Collections.Generic;

namespace RiotPls.DataDragon.Entities
{
    public class LevelTip
    {
        public IReadOnlyCollection<string> Labels { get; }
        
        public IReadOnlyCollection<string> Effects { get; }
        
        internal LevelTip(LevelTipDto dto)
        {
            Labels = dto.Labels;
            Effects = dto.Effects;
        }
    }
}