using System;
using System.Collections.Generic;
using RiotPls.DataDragon.Entities;

namespace RiotPls.DataDragon
{
    /// <summary>
    ///     Configuration options for a <see cref="DataDragonClient"/>.
    /// </summary>
    public class DataDragonClientOptions
    {
        /// <summary>
        ///     The cache options for version data. Defaults to expiring every 14 days.
        /// </summary>
        public CacheControl<IReadOnlyCollection<GameVersion>> Versions { get; set; } 
            = CacheControl<IReadOnlyCollection<GameVersion>>.TimedCache(TimeSpan.FromDays(14));
        
        /// <summary>
        ///     The cache options for language data. Defaults to permanent cache.
        /// </summary>
        public CacheControl<IReadOnlyCollection<string>> Languages { get; set; } 
            = CacheControl<IReadOnlyCollection<string>>.Permanent;
        
        /// <summary>
        ///     The cache options for champions data. Defaults to expiring every 30 days.
        /// </summary>
        public CacheControl<ChampionData> Champions { get; set; } 
            = CacheControl<ChampionData>.TimedCache(TimeSpan.FromDays(30));
    }
}