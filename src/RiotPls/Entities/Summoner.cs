using System;
using System.Linq;
using RiotPls.DataDragon.Entities;
using RiotPls.Entities.DataObject;

namespace RiotPls.Entities
{
    public sealed class Summoner
    {
        private readonly LeagueClient _client;

        /// <summary>
        ///     Encrypted account ID. Max length 56 characters. 
        /// </summary>
        public string AccountId { get; }
        
        /// <summary>
        ///     ID of the summoner icon associated with the summoner. 
        /// </summary>
        public int ProfileIconId { get; }
        
        /// <summary>
        ///     Profile icon associated with the summoner.
        /// </summary>
        public ProfileIcon? ProfileIcon { get; }
        
        /// <summary>
        ///     Date summoner was last modified specified as epoch milliseconds.
        ///     The following events will update this timestamp: summoner name change, summoner level change, or profile icon change.
        /// </summary>
        public DateTimeOffset RevisionDate { get; }
        
        /// <summary>
        ///     Summoner name.
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        ///     Encrypted summoner ID. Max length 63 characters.
        /// </summary>
        public string Id { get; }
        
        /// <summary>
        ///     Encrypted PUUID. Exact length of 78 characters.
        /// </summary>
        public string Puuid { get; }
        
        /// <summary>
        ///     Summoner level associated with the summoner.
        /// </summary>
        public long Level { get; }

        internal Summoner(LeagueClient client, SummonerDto dto)
        {
            _client = client;
            
            AccountId = dto.AccountId;
            ProfileIconId = dto.ProfileIconId;
            ProfileIcon = LeagueClient.DataDragonDataContainer?.ProfileIcons?.FirstOrDefault(x => x.Id == dto.ProfileIconId);
            RevisionDate = DateTimeOffset.FromUnixTimeMilliseconds(dto.RevisionDate);
            Name = dto.Name;
            Id = dto.Id;
            Puuid = dto.Puuid;
            Level = dto.Level;
        }
    }
}