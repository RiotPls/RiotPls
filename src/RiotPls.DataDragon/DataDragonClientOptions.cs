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

    /// <summary>
    ///     Represents preferred options for caching data.
    /// </summary>
    public class CacheControl<T> where T : class
    {
        /// <summary>
        ///     Instructs the client to not cache this data.
        /// </summary>
        public static CacheControl<T> None 
            => new CacheControl<T>(false);
        
        /// <summary>
        ///     Instructs the client to cache this data, using the default expiry time of 24 hours.
        /// </summary>
        public static CacheControl<T> Default 
            => new CacheControl<T>(true, TimeSpan.FromHours(24));

        /// <summary>
        ///     Instructs the client to cache this data permanently.
        /// </summary>
        public static CacheControl<T> Permanent 
            => new CacheControl<T>(true);

        /// <summary>
        ///     Instructs the client to cache this data for the specified expiry time.
        /// </summary>
        /// <param name="cacheExpiry">
        ///     The time to hold this data for before requesting it again.
        /// </param>
        /// <returns>
        ///     A cache control instruction set representing the provided parameters.
        /// </returns>
        public static CacheControl<T> TimedCache(TimeSpan cacheExpiry)
        {
            return new CacheControl<T>(true, cacheExpiry);
        }

        internal bool IsCached { get; }
        internal TimeSpan? CacheExpiry { get; }
        internal bool IsPermanent => CacheExpiry == null;

        private T _data;

        internal DateTimeOffset? LastSetTime;

        internal bool IsExpired
        {
            get
            {
                if (!IsCached) return true;
                if (_data is null || LastSetTime is null) return true;
                if (CacheExpiry is null) return false;
                return DateTimeOffset.Now.ToUnixTimeSeconds() >
                       LastSetTime.Value.Add(CacheExpiry.Value).ToUnixTimeSeconds();
            }
        }
        
        internal T Data
        {
            get => IsExpired ? null : _data;
            set
            {
                _data = value;
                LastSetTime = DateTimeOffset.Now;
            }
        }

        /// <summary>
        ///     Creates a new cache control.
        /// </summary>
        /// <param name="cache">
        ///     Whether to instruct the client to cache this data.
        /// </param>
        /// <param name="cacheExpiry">
        ///     The time to hold this data for before requesting it again.
        ///     If null, the data will be held indefinitely.
        /// </param>
        public CacheControl(bool cache, TimeSpan? cacheExpiry = null)
        {
            IsCached = cache;
            CacheExpiry = cacheExpiry;
        }
    }
}