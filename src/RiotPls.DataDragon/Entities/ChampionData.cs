using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    public sealed class ChampionData : BaseData
    {
        /// <summary>
        ///     Full champion data.
        /// </summary>
        public Champion Champion { get; }
        
        internal ChampionData(DataDragonClient client, ChampionDataDto dto) : base(client, dto)
        {
            Champion = new Champion(client, dto.Champion.Values.First());
        }
    }
}