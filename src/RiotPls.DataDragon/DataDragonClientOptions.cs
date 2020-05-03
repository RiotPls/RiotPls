﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RiotPls.DataDragon.Entities;

namespace RiotPls.DataDragon
{
    /// <summary>
    ///     Configuration options for a <see cref="DataDragonClient"/>.
    /// </summary>
    public sealed class DataDragonClientOptions
    {
        /// <summary>
        ///     The cache options for version data. Defaults to expiring every 14 days.
        /// </summary>
        public CacheControl<IReadOnlyList<GameVersion>> Versions { get; set; } 
            = CacheControl<IReadOnlyList<GameVersion>>.TimedCache(TimeSpan.FromDays(14));
        
        /// <summary>
        ///     The cache options for language data. Defaults to permanent cache.
        /// </summary>
        public CacheControl<IReadOnlyCollection<string>> Languages { get; set; } 
            = CacheControl<IReadOnlyCollection<string>>.Permanent;
        
        /// <summary>
        ///     The cache options for champions data. Defaults to expiring every 30 days.
        /// </summary>
        public CacheControl<ChampionBaseData> BaseChampions { get; set; } 
            = CacheControl<ChampionBaseData>.TimedCache(TimeSpan.FromDays(30));
        
        //todo: review this since it's kinda different from others.
        /// <summary>
        ///     The cache options for full champions data.
        /// </summary>
        public IReadOnlyDictionary<string, CacheControl<ChampionData>> Champions { get; }
        internal ConcurrentDictionary<string, CacheControl<ChampionData>> _champions;

        /// <summary>
        ///     Duration of the cache for full champions data.
        /// </summary>
        public TimeSpan ChampionFullCacheDuration { get; set; }
            = TimeSpan.FromDays(30);

        public DataDragonClientOptions()
        {
            _champions = new ConcurrentDictionary<string, CacheControl<ChampionData>>();
            Champions = new ReadOnlyDictionary<string, CacheControl<ChampionData>>(_champions);
        }
    }
}