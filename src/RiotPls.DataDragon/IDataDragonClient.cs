using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RiotPls.DataDragon.Entities;

namespace RiotPls.DataDragon
{
    public interface IDataDragonClient : IDisposable
    {
        GameLanguage DefaultLanguage { get; }
        ValueTask<IReadOnlyList<GameVersion>> GetVersionsAsync();
        ValueTask<IReadOnlyList<GameLanguage>> GetLanguagesAsync();
        ValueTask<ChampionBaseData> GetPartialChampionsAsync(GameVersion version, GameLanguage language);
        ValueTask<ChampionFullData> GetChampionsAsync(GameVersion version, GameLanguage language);
        ValueTask<ChampionData> GetChampionAsync(string championName, GameVersion version, GameLanguage language);
        ValueTask<SummonerSpellData> GetSummonerSpellsAsync(GameVersion version, GameLanguage language);
        ValueTask<ProfileIconData> GetProfileIconsAsync(GameVersion version, GameLanguage language);

        public ValueTask<ChampionBaseData> GetPartialChampionsAsync(GameVersion version)
            => GetPartialChampionsAsync(version, DefaultLanguage);

        public ValueTask<ChampionFullData> GetChampionsAsync(GameVersion version)
            => GetChampionsAsync(version, DefaultLanguage);

        public ValueTask<ChampionData> GetChampionAsync(string championName, GameVersion version)
            => GetChampionAsync(championName, version, DefaultLanguage);

        public ValueTask<SummonerSpellData> GetSummonerSpellsAsync(GameVersion version)
            => GetSummonerSpellsAsync(version, DefaultLanguage);

        public ValueTask<ProfileIconData> GetProfileIconsAsync(GameVersion version)
            => GetProfileIconsAsync(version, DefaultLanguage);
    }
}