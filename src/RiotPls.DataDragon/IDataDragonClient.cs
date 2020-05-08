using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RiotPls.DataDragon.Entities;
using RiotPls.DataDragon.Enums;

namespace RiotPls.DataDragon
{
    public interface IDataDragonClient : IDisposable
    {
        /// <summary>
        ///     Gets the default language of the client.
        /// </summary>
        Language DefaultLanguage { get; }

        /// <summary>
        ///     Gets the current version of this client's realm.
        /// </summary>
        RealmVersion RealmVersion { get; }

        /// <summary>
        ///     Fetches the latests <see cref="RealmVersion"/> and updates it.
        /// </summary>
        /// <returns>
        ///    A task object representing the asynchronous operation.
        /// </returns>
        Task UpdateRealmVersionAsync();
        /// <summary>
        ///    Returns a list of every available version of Data Dragon.
        /// </summary>
        ValueTask<IReadOnlyList<GameVersion>> GetVersionsAsync();
        ValueTask<IReadOnlyList<Language>> GetLanguagesAsync();
        ValueTask<ChampionBaseData> GetPartialChampionsAsync(GameVersion version, Language language);
        ValueTask<ChampionFullData> GetChampionsAsync(GameVersion version, Language language);
        ValueTask<ChampionData> GetChampionAsync(ChampionId championName, GameVersion version, Language language);
        ValueTask<SummonerSpellData> GetSummonerSpellsAsync(GameVersion version, Language language);
        ValueTask<ProfileIconData> GetProfileIconsAsync(GameVersion version, Language language);

        public ValueTask<ChampionBaseData> GetPartialChampionsAsync(GameVersion version)
            => GetPartialChampionsAsync(version, DefaultLanguage);

        public ValueTask<ChampionFullData> GetChampionsAsync(GameVersion version)
            => GetChampionsAsync(version, DefaultLanguage);

        public ValueTask<ChampionData> GetChampionAsync(ChampionId championName, GameVersion version)
            => GetChampionAsync(championName, version, DefaultLanguage);

        public ValueTask<SummonerSpellData> GetSummonerSpellsAsync(GameVersion version)
            => GetSummonerSpellsAsync(version, DefaultLanguage);

        public ValueTask<ProfileIconData> GetProfileIconsAsync(GameVersion version)
            => GetProfileIconsAsync(version, DefaultLanguage);
    }
}