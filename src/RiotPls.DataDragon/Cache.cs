using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using RiotPls.DataDragon.Entities;
using RiotPls.DataDragon.Enums;

namespace RiotPls.DataDragon
{
    internal class Cache<T>
    {
        public static Cache<T> Instance { get; } = new Cache<T>();

        public T this[GameVersion version, Language language]
        {
            set => _cache[(version, language)] = value;
        }

        private readonly Dictionary<(GameVersion, Language), T> _cache;

        public Cache()
        {
            _cache = new Dictionary<(GameVersion, Language), T>();
        }

        public void Clear()
            => _cache.Clear();

        public bool TryGetValue((GameVersion, Language) key, [NotNullWhen(true)] out T value)
            => _cache.TryGetValue(key, out value!);

        public void AddOrUpdate(GameVersion version, Language language, T value)
            => _cache[(version, language)] = value;
    }

    internal static class ChampionCache
    {
        private static readonly Dictionary<ChampionId, Cache<ChampionData>> _cache;

        static ChampionCache()
        {
            _cache = new Dictionary<ChampionId, Cache<ChampionData>>();
        }

        public static void Clear()
            => _cache.Clear();

        public static bool TryGetValue(ChampionId key, [NotNullWhen(true)] out Cache<ChampionData> value)
            => _cache.TryGetValue(key, out value!);

        public static void AddOrUpdate(ChampionId key, GameVersion version, Language language, ChampionData value)
        {
            if (_cache.TryGetValue(key, out var cache))
                cache[version, language] = value;
            else
            {
                cache = Cache<ChampionData>.Instance;
                cache[version, language] = value;
                _cache[key] = cache;
            }
        }
    }
}