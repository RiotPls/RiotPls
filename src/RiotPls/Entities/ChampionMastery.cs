using System;
using RiotPls.DataDragon.Entities;
using RiotPls.Entities.DataObject;

namespace RiotPls.Entities
{
    public sealed class ChampionMastery
    {
        private readonly LeagueClient _client;

        /// <summary>
        ///     Number of points needed to achieve next level.
        ///     Zero if player reached maximum champion level for this champion.
        /// </summary>
        public long PointsUntilNextLevel { get; }
        
        /// <summary>
        ///     Is chest granted for this champion or not in current season.
        /// </summary>
        public bool ChestGranted { get; }
        
        /// <summary>
        ///     Champion ID for this entry.
        /// </summary>
        public long ChampionId { get; }
        
        /// <summary>
        ///     Champion for this entry.
        /// </summary>
        public Champion? Champion { get; }
        
        /// <summary>
        ///     Last time this champion was played by this player - in Unix milliseconds time format.
        /// </summary>
        public DateTimeOffset LastPlayTime { get; }
        
        /// <summary>
        ///     Champion level for specified player and champion combination.
        /// </summary>
        public int Level { get; }
        
        /// <summary>
        ///     Summoner ID for this entry. (Encrypted)
        /// </summary>
        public string SummonerId { get; }
        
        /// <summary>
        ///     Total number of champion points for this player and champion combination - they are used to determine championLevel.
        /// </summary>
        public int Points { get; }
        
        /// <summary>
        ///     Number of points earned since current level has been achieved.
        /// </summary>
        public long PointsSinceLastLevel { get; }
        
        /// <summary>
        ///     The token earned for this champion to levelup.
        /// </summary>
        public int TokensEarned { get; }

        internal ChampionMastery(LeagueClient client, ChampionMasteryDto dto)
        {
            _client = client;
            
            PointsUntilNextLevel = dto.PointsUntilNextLevel;
            ChestGranted = dto.ChestGranted;
            ChampionId = dto.ChampionId;
            Champion = null; // todo: get it from DataDragon
            LastPlayTime = DateTimeOffset.FromUnixTimeMilliseconds(dto.LastPlayTime);
            Level = dto.Level;
            SummonerId = dto.SummonerId;
            Points = dto.Points;
            PointsSinceLastLevel = dto.PointsSinceLastLevel;
            TokensEarned = dto.TokensEarned;
        }
    }
}