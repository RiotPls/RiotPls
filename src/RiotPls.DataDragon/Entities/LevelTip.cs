using System.Collections.Generic;

namespace RiotPls.DataDragon.Entities
{
    // todo: find a better way to handle when its optional, so null.
    public sealed class LevelTip
    {
        public IReadOnlyCollection<string> Labels { get; }
            = new List<string>();
        
        public IReadOnlyCollection<string> Effects { get; }
            = new List<string>();
        
        internal LevelTip(LevelTipDto dto)
        {
            if (dto is null)
            {
                return;
            }
            
            Labels = dto.Labels;
            Effects = dto.Effects;
        }
    }
}