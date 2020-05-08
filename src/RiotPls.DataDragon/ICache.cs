using System;
using System.Diagnostics.CodeAnalysis;
using RiotPls.DataDragon.Entities;

namespace RiotPls.DataDragon
{
    /// <summary>
    ///     Represents a cache manager for <typeparamref name="T"/>. 
    ///     The implementation of this <see langword="interface"/> should be thread-safe.
    /// </summary>
    /// <typeparam name="T">
    ///     The type to be cached.
    /// </typeparam>
    public interface ICache<T> where T : class
    {
        /// <summary>
        /// Gets or sets the data for the provided version.
        /// </summary>
        /// <param name="version">
        ///     The version of the data.
        /// </param>
        /// <returns>
        ///     The cached data.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when you attempt to get the data from a version that isn't cached 
        ///     or attempt to overwrite existing data.
        /// </exception>
        T this[GameVersion version]
        {
            get => GetData(version);
            set => AddData(version, value);
        }
        /// <summary>
        ///     Indicates if there's data available for the providen version.
        /// </summary>
        /// <param name="version">
        ///     The version of the data.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if there's data for the providen version. Otherwise <see langword="false"/>.
        /// </returns>
        bool Contains(GameVersion version);
        /// <summary>
        ///     Gets the data that is currently cached for the providen version.
        /// </summary>
        /// <param name="version">
        ///     The version of the data.
        /// </param>
        /// <returns>
        ///     The cached data.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when there's no cached data for the providen version.
        /// </exception>
        T GetData(GameVersion version);

        /// <summary>
        ///     Attempts the data that is currently cached for the providen version.
        /// </summary>
        /// <param name="version">
        ///     The version of the data.
        /// </param>
        /// <param name="data">
        ///     The cached data.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the data is cached. Otherwise <see langword="false"/>.
        /// </returns>
        bool TryGetData(GameVersion version,[NotNullWhen(true)] out T? data);

        /// <summary>
        ///     Adds the data to the cache.
        /// </summary>
        /// <param name="version">
        ///     The version of the data.
        /// </param>
        /// <param name="Data">
        ///     The data.
        /// </param>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the data for that version already exists.
        /// </exception>
        void AddData(GameVersion version, T data);

        /// <summary>
        ///     Attempts to add the data to the cache.
        /// </summary>
        /// <param name="version">
        ///     The version of the data.
        /// </param>
        /// <param name="data">
        ///     The data.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the data is added. <see langword="false"/> if it already exists.
        /// </returns>
        bool TryAddData(GameVersion version, T data);

        /// <summary>
        ///     Clears the cache.
        /// </summary>
        void Clear();

        /// <summary>
        ///     Clears the cache for all versions older than the providen one.
        /// </summary>
        /// <param name="version">
        ///     The version.
        /// </param>
        void ClearOldData(GameVersion version);

        /// <summary>
        ///     Removes from the cache the entry with the providen version.
        /// </summary>
        /// <param name="version">
        ///     The version.
        /// </param>
        void Remove(GameVersion version);
    }
}