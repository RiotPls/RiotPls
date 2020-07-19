using System;
using System.Collections.Concurrent;
using System.Linq;
using RiotPls.DataDragon.Enums;

namespace RiotPls.DataDragon.Entities
{
    internal class ChampionFullData : BaseData
    {
        /// <summary>
        ///     A dictionary of champion objects, keyed by their unique identifiers.
        /// </summary>
        public ConcurrentDictionary<ChampionId, Champion> Champions { get; }

        internal ChampionFullData(ChampionFullDataDto dto, bool createEmpty = false) : base(dto)
        {
            Champions = createEmpty 
                ? new ConcurrentDictionary<ChampionId, Champion>() 
                : new ConcurrentDictionary<ChampionId, Champion>(
                    dto.Champions.Values.ToDictionary(
                        x => Enum.Parse<ChampionId>(x.Id, true),
                        Converter));
        }

        private Champion Converter(ChampionDto dto)
            => new Champion(dto, Version);
    }
}