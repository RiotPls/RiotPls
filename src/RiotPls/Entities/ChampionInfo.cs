using System.Collections.Generic;
using System.Collections.Immutable;
using RiotPls.Entities.DataObject;

namespace RiotPls.Entities
{
    public sealed class ChampionInfo
    {
        private readonly LeagueClient _client;
        
        /// <summary>
        ///     Maximum level for a player to get the free champions for new players.
        /// </summary>
        public int MaxNewPlayerLevel { get; }
        
        /// <summary>
        ///     Set of free champion ids rotation for new players.
        /// </summary>
        public IReadOnlyCollection<int> FreeChampionIdsForNewPlayers { get; }
        
        /// <summary>
        ///     Set of free champion ids rotation for players.
        /// </summary>
        public IReadOnlyCollection<int> FreeChampionIds { get; }

        internal ChampionInfo(LeagueClient client, ChampionInfoDto dto)
        {
            _client = client;
            
            MaxNewPlayerLevel = dto.MaxNewPlayerLevel;
            FreeChampionIdsForNewPlayers = dto.FreeChampionIdsForNewPlayers.ToImmutableArray();
            FreeChampionIds = dto.FreeChampionIds.ToImmutableArray();
            // todo: expose Champions directly instead of ids.
        }
    }
}