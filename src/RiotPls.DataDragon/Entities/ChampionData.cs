using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    public class ChampionData : BaseData
    {
        /// <summary>
        ///     Full champion data.
        /// </summary>
        public Champion Champion { get; }
        
        internal ChampionData(ChampionDataDto dto) : base(dto)
        {
            Champion = new Champion(dto.Champion.Values.First());
        }
    }
}