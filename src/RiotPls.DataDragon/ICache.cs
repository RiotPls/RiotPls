namespace RiotPls.DataDragon
{
    /// <summary>
    ///     Represents a <see langword="class"/> which can be used to cache data. 
    ///     The implementation of this <see langword="interface"/> should be thread-safe.
    /// </summary>
    /// <typeparam name="T">
    ///     The type to be cached.
    /// </typeparam>
    public interface ICache<T> where T : class
    {
        ICacheExpiryPolicy ExpiryPolicy {get;}
        /// <summary>
        ///     Indicates if the cache has expired.
        /// </summary>
        bool IsExpired { get; }

        /// <summary>
        ///     Gets the data that may be cached.
        /// </summary>
        T? Data { get; set; }
    }
}