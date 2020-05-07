using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    public sealed class ChampionData : BaseData
    {
        /// <summary>
        ///     Full champion data.
        /// </summary>
        public Champion Champion { get; }
        
        internal ChampionData(ChampionDataDto dto) : base(dto)
        {
            // Only one value is expected so this is a good use-case for Single() to keep the integrity of the data.
            Champion = new Champion(dto.Champion.Values.Single()); 
        }
    }
}