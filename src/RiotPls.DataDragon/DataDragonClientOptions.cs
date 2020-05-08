using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RiotPls.DataDragon.Entities;
using RiotPls.DataDragon.Enums;

namespace RiotPls.DataDragon
{
    /// <summary>
    ///     Configuration options for a <see cref="DataDragonClient"/>.
    /// </summary>
    public sealed class DataDragonClientOptions
    {
        /// <summary>
        ///     Gets or sets the default <see cref="Language"/> to use
        ///     when doing requests to the Data Dragon static API.
        /// </summary>
        public Language DefaultLanguage { get; set; }
            = Language.AmericanEnglish;

        /// <summary>
        ///     The cache options for version data. Defaults to expiring every every day.
        /// </summary>
        public ICache<IReadOnlyList<GameVersion>> Versions { get; set; }
            = CacheControl<IReadOnlyList<GameVersion>>.TimedCache(TimeSpan.FromDays(1));

        /// <summary>
        ///     The cache options for language data. Defaults to permanent cache.
        /// </summary>
        public ICache<IReadOnlyList<Language>> Languages { get; set; }
            = CacheControl<IReadOnlyList<Language>>.Permanent;

        /// <summary>
        ///     The cache options for champions data. Defaults to expiring every day.
        /// </summary>
        public ICache<ChampionBaseData> PartialChampions { get; set; }
            = CacheControl<ChampionBaseData>.TimedCache(TimeSpan.FromDays(1));

        /// <summary>
        ///     The cache options for champions data. Defaults to expiring every day.
        /// </summary>
        public ICache<ChampionFullData> FullChampions { get; set; }
            = CacheControl<ChampionFullData>.TimedCache(TimeSpan.FromDays(1));

        /// <summary>
        ///     The cache options for summoner spells data. Defaults to 30 days cache.
        /// </summary>
        public ICache<SummonerSpellData> SummonerSpells { get; set; }
            = CacheControl<SummonerSpellData>.TimedCache(TimeSpan.FromDays(30));
        
        /// <summary>
        ///     The cache options for maps data. Defaults to permanent cache.
        /// </summary>
        public ICache<MapData> Maps { get; set; }
            = CacheControl<MapData>.Permanent;
        
        /// <summary>
        ///     The cache options for mission assets data. Defaults to permanent cache.
        /// </summary>
        public ICache<MissionAssetData> MissionAssets { get; set; }
            = CacheControl<MissionAssetData>.TimedCache(TimeSpan.FromDays(14));
        
        /// <summary>
        ///     The cache options for runes data. Defaults to 14 days cache.
        /// </summary>
        public ICache<IReadOnlyList<Rune>> Runes { get; set; }
            = CacheControl<IReadOnlyList<Rune>>.TimedCache(TimeSpan.FromDays(14));

        /// <summary>
        ///     The cache options for profile icons data. Defaults to 30 days cache.
        /// </summary>
        public ICache<ProfileIconData> ProfileIcons { get; set; }
            = CacheControl<ProfileIconData>.TimedCache(TimeSpan.FromDays(30));

        // todo: review this since it's kinda different from others.
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