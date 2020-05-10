namespace RiotPls.DataDragon.Enums
{
    /// <summary>
    ///     Represents the cache behavior of the client.
    /// </summary>
    public enum CacheMode
    {
        /// <summary>
        ///     Instructs the client not to cache anything.
        /// </summary>
        None,

        /// <summary>
        ///     Instructs the client to cache only the most up-to-date data. 
        /// </summary>
        MostRecentOnly,

        /// <summary>
        ///     Instructs the client to cache everything.
        /// </summary>
        KeepAll
    }
}