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

        internal ChampionFullData(ChampionFullDataDto dto) : base(dto)
        {
            Champions = new ConcurrentDictionary<ChampionId, Champion>(
                dto.Champions.Values.ToDictionary(
                    x => Enum.Parse<ChampionId>(x.Id, true), 
                    x => new Champion(x)));
        }
    }
}