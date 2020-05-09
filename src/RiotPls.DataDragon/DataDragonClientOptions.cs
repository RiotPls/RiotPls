using RiotPls.DataDragon.Enums;

namespace RiotPls.DataDragon
{
    /// <summary>
    ///     Configuration options for a <see cref="DataDragonClient"/>.
    /// </summary>
    public sealed class DataDragonClientOptions
    {
        /// <summary>
        ///     Gets a <see langword="new"/> instance of <see cref="DataDragonClient"/> with the <see langword="default"/> values.
        /// </summary>
        public static DataDragonClientOptions Default => new DataDragonClientOptions();

        /// <summary>
        ///     Gets or sets the default <see cref="Language"/> to use
        ///     when doing requests to the Data Dragon static API.
        /// </summary>
        public Language DefaultLanguage { get; set; }

        /// <summary>
        ///     Gets or sets the cache mode to use in this client.
        /// </summary>
        public CacheMode CacheMode { get; set; }
    }
}