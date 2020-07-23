using System.Collections.Generic;
using System.Collections.Immutable;
using RiotPls.Entities.DataObject;

namespace RiotPls.Entities
{
    public sealed class ChampionInfo
    {
        public int MaxNewPlayerLevel { get; }
        
        public IReadOnlyCollection<int> FreeChampionIdsForNewPlayers { get; }
        
        public IReadOnlyCollection<int> FreeChampionIds { get; }

        internal ChampionInfo(ChampionInfoDto dto)
        {
            MaxNewPlayerLevel = dto.MaxNewPlayerLevel;
            FreeChampionIdsForNewPlayers = dto.FreeChampionIdsForNewPlayers.ToImmutableArray();
            FreeChampionIds = dto.FreeChampionIds.ToImmutableArray();
            // todo: expose Champions directly instead of ids.
        }
    }
}