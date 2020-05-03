using System;

namespace RiotPls.DataDragon
{
    /// <summary>
    ///     Represents a <see langword="class"/> or <see langword="struct"/> 
    ///     which holds information about the expiry policy of an <see cref="ICache{T}"/>.
    /// </summary>
    public interface ICacheExpiryPolicy
    {
        /// <summary>
        ///     Indicates if the cache value is cached.
        /// </summary>
        bool IsCached { get; }

        /// <summary>
        ///     Indicates if the value is cached permanently.
        /// </summary>
        bool IsPermanent { get; }

        /// <summary>
        ///     Indicates the amount of time missing for the cache to expire. 
        ///     Or <see langword="null"/> if the cache is permanent.
        /// </summary>
        TimeSpan? CacheExpiry { get; }

        /// <summary>
        ///     Indicates the last moment the cache was updated.
        /// </summary>
        DateTimeOffset? LastSetTime { get; set; }
    }
}