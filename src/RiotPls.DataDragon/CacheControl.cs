using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using RiotPls.DataDragon.Entities;

namespace RiotPls.DataDragon
{
    /// <summary>
    ///     Represents a thread-safe cache manager for <typeparamref name="T"/>.
    /// </summary>
    public sealed class CacheControl<T> : ICache<T> where T : class
    {
        internal static CacheControl<T> Instance { get; } = new CacheControl<T>();

        private readonly Dictionary<GameVersion, T> _cache;
        private readonly object _lock;

        public CacheControl()
        {
            _cache = new Dictionary<GameVersion, T>();
            _lock = new object();
        }

        /// <inheritdoc/>
        public bool Contains(GameVersion version)
        { 
            lock (_lock)
                return _cache.ContainsKey(version);
        }

        /// <inheritdoc/>
        public T GetData(GameVersion version)
        {
            lock (_lock)
            {
                if (!Contains(version))
                    throw new InvalidOperationException($"The cache does not contain data for the version {version}");

                return _cache[version];
            }
        }

        /// <inheritdoc/>
        public bool TryGetData(GameVersion version, [NotNullWhen(true)] out T? data)
        {
            lock (_lock)
                return _cache.TryGetValue(version, out data);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            lock (_lock)
                _cache.Clear();
        }

        public void ClearOldData(GameVersion version)
        {
            lock (_lock)
            {
                var keys = _cache.Keys.Where(key => key < version);

                foreach (var key in keys)
                    Remove(key);
            }
        }

        /// <inheritdoc/>
        public void Remove(GameVersion version)
        {
            lock (_lock)
                _cache.Remove(version);
        }

        /// <inheritdoc/>
        public void AddData(GameVersion version, T data)
        {
            lock (_lock)
            {
                if (!_cache.TryAdd(version, data))
                    throw new InvalidOperationException("There's already data for the version {version}");
            }
        }

        /// <inheritdoc/>
        public bool TryAddData(GameVersion version, T data)
        {
            lock (_lock)
                return _cache.TryAdd(version, data);
        }
    }
}