using System;

namespace RiotPls.DataDragon
{
    /// <summary>
    ///     Represents preferred options for caching data.
    /// </summary>
    public sealed class CacheControl<T> : ICache<T> where T : class
    {
        /// <summary>
        ///     Instructs the client to not cache this data.
        /// </summary>
        public static CacheControl<T> None
            => new CacheControl<T>(CacheControlExpiryPolicy.None);

        /// <summary>
        ///     Instructs the client to cache this data, using the default expiry time of 24 hours.
        /// </summary>
        public static CacheControl<T> Default
            => new CacheControl<T>(CacheControlExpiryPolicy.Default);

        /// <summary>
        ///     Instructs the client to cache this data permanently.
        /// </summary>
        public static CacheControl<T> Permanent
            => new CacheControl<T>(CacheControlExpiryPolicy.Permanent);

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
            => new CacheControl<T>(CacheControlExpiryPolicy.TimedCache(cacheExpiry));

        private T? _data;

        /// <summary>
        ///     Gets the expiry policy for this cache.
        /// </summary>
        public ICacheExpiryPolicy ExpiryPolicy { get; }

        /// <inheritdoc/>
        public bool IsExpired
        {
            get
            {
                if (!ExpiryPolicy.IsCached)
                    return true;

                if (_data is null || ExpiryPolicy.LastSetTime is null)
                    return true;

                if (ExpiryPolicy.CacheExpiry is null)
                    return false;

                return DateTimeOffset.Now.ToUnixTimeSeconds() >
                       ExpiryPolicy.LastSetTime.Value.Add(ExpiryPolicy.CacheExpiry.Value).ToUnixTimeSeconds();
            }
        }

        /// <inheritdoc/>
        public T? Data
        {
            get => IsExpired
                ? null
                : _data;
            set
            {
                _data = value;
                ExpiryPolicy.LastSetTime = DateTimeOffset.Now;
            }
        }

        /// <summary>
        ///     Creates a new cache control.
        /// </summary>
        /// <param name="expiryPolicy">
        ///     The expiry policy to be used in this cache.
        /// </param>
        public CacheControl(ICacheExpiryPolicy expiryPolicy)
        {
            if (expiryPolicy is null)
                throw new ArgumentNullException(nameof(expiryPolicy));

            ExpiryPolicy = expiryPolicy;
        }

        // Don't move this class to another file. This class is meant to be nested.

        /// <summary>
        ///     Default implementation of <see cref="ICacheExpiryPolicy"/>.
        /// </summary>
        public sealed class CacheControlExpiryPolicy : ICacheExpiryPolicy
        {
            /// <inheritdoc cref="CacheControl{T}.None"/>
            public static ICacheExpiryPolicy None
                => new CacheControlExpiryPolicy(false);

            /// <inheritdoc cref="CacheControl{T}.Default"/>
            public static ICacheExpiryPolicy Default
                => new CacheControlExpiryPolicy(true, TimeSpan.FromHours(24));

            /// <inheritdoc cref="CacheControl{T}.Permanent"/>
            public static ICacheExpiryPolicy Permanent
                => new CacheControlExpiryPolicy(true);

            /// <inheritdoc cref="CacheControl{T}.TimedCache(TimeSpan)"/>
            public static ICacheExpiryPolicy TimedCache(TimeSpan cacheExpiry)
                => new CacheControlExpiryPolicy(true, cacheExpiry);


            /// <inheritdoc/>
            public bool IsCached { get; }

            /// <inheritdoc/>
            public bool IsPermanent
                => CacheExpiry is null;

            /// <inheritdoc/>
            public DateTimeOffset? LastSetTime { get; set; }

            /// <inheritdoc/>
            public TimeSpan? CacheExpiry { get; }

            internal CacheControlExpiryPolicy(bool isCached) : this(isCached, null)
            {
            }

            internal CacheControlExpiryPolicy(bool isCached, TimeSpan? cacheExpiry)
            {
                IsCached = isCached;
                CacheExpiry = cacheExpiry;
            }
        }
    }
}