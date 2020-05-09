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
        ///    Gets a list from the cache of every available version of Data Dragon.
        ///    If the list is not available it will be fetched from the API.
        /// </summary>
        ValueTask<IReadOnlyList<GameVersion>> GetVersionsAsync();

        /// <summary>
        ///     Fetches a list from the API of every available version of Data Dragon.
        /// </summary>
        Task<IReadOnlyList<GameVersion>> FetchVersionsAsync();

        /// <summary>
        ///    Gets a list from the cache of every available language for the latest version of Data Dragon.
        ///    If the list is not available it will be fetched from the API.
        /// </summary>
        ValueTask<IReadOnlyList<Language>> GetLanguagesAsync();

        /// <summary>
        ///     Fetches a list from the API of every available language of Data Dragon.
        /// </summary>
        Task<IReadOnlyList<Language>> FetchLanguagesAsync();

        /// <summary>
        ///     Fetches the latest version of Data Dragon.
        /// </summary>
        Task<GameVersion> FetchLatestVersionAsync();

        /// <summary>
        ///     Gets a <see cref="ChampionBaseData"/> from the cache. If the data is not cached it will be fetched from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is providen.
        /// </param>
        ValueTask<ChampionBaseData> GetPartialChampionsAsync(GameVersion version, Language? language);

        /// <summary>
        ///     Fetches a <see cref="ChampionBaseData"/> from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use. The lastest version will be fetched if <see langword="null"/> is providen.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is providen.
        /// </param>
        Task<ChampionBaseData> FetchPartialChampionAsync(GameVersion? version, Language? language);

        /// <summary>
        ///     Gets a <see cref="ChampionFullData"/> from the cache. If the data is not cached it will be fetched from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is providen.
        /// </param>
        ValueTask<ChampionFullData> GetChampionsAsync(GameVersion version, Language? language);

        /// <summary>
        ///     Fetches a <see cref="ChampionFullData"/> from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use. The lastest version will be fetched if <see langword="null"/> is providen.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is providen.
        /// </param>
        Task<ChampionFullData> FetchChampionsAsync(GameVersion? version, Language? language);

        /// <summary>
        ///     Gets a <see cref="ChampionData"/> from the cache. If the data is not cached it will be fetched from the API.
        /// </summary>
        /// <param name="championId">
        ///     The id of the Champion.
        /// </param>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is providen.
        /// </param>
        ValueTask<ChampionData> GetChampionAsync(ChampionId championId, GameVersion version, Language? language);

        /// <summary>
        ///     Fetches a <see cref="ChampionData"/> from the API.
        /// </summary>
        /// <param name="championId">
        ///     The id of the Champion.
        /// </param>
        /// <param name="version">
        ///     The version of Data Dragon to use. The lastest version will be fetched if <see langword="null"/> is providen.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is providen.
        /// </param>
        Task<ChampionData> FetchChampionAsync(ChampionId championId, GameVersion? version, Language? language);

        /// <summary>
        ///     Gets a <see cref="SummonerSpellData"/> from the cache. If the data is not cached it will be fetched from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is providen.
        /// </param>
        ValueTask<SummonerSpellData> GetSummonerSpellsAsync(GameVersion version, Language? language);

        /// <summary>
        ///     Fetches a <see cref="SummonerSpellData"/> from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use. The lastest version will be fetched if <see langword="null"/> is providen.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is providen.
        /// </param>
        Task<SummonerSpellData> FetchSummonerSpellsAsync(GameVersion? version, Language? language);

        /// <summary>
        ///     Gets a <see cref="ProfileIconData"/> from the cache. If the data is not cached it will be fetched from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is providen.
        /// </param>
        ValueTask<ProfileIconData> GetProfileIconsAsync(GameVersion version, Language? language);

        /// <summary>
        ///     Fetches a <see cref="ProfileIconData"/> from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use. The lastest version will be fetched if <see langword="null"/> is providen.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is providen.
        /// </param>
        Task<ProfileIconData> FetchProfileIconsAsync(GameVersion? version, Language? language);

        /// <summary>
        ///     Gets a <see cref="MapData"/> from the cache. If the data is not cached it will be fetched from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is providen.
        /// </param>
        ValueTask<MapData> GetMapsAsync(GameVersion version, Language? language);

        /// <summary>
        ///     Fetches a <see cref="MapData"/> from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use. The lastest version will be fetched if <see langword="null"/> is providen.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is providen.
        /// </param>
        Task<MapData> FetchMapsAsync(GameVersion? version, Language? language);

        /// <summary>
        ///     Gets a <see cref="IReadOnlyList{Rune}"/> from the cache. If the data is not cached it will be fetched from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is providen.
        /// </param>
        ValueTask<IReadOnlyList<Rune>> GetRunesAsync(GameVersion version, Language? language);

        /// <summary>
        ///     Fetches a <see cref="IReadOnlyList{Rune}"/> from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use. The lastest version will be fetched if <see langword="null"/> is providen.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is providen.
        /// </param>
        Task<IReadOnlyList<Rune>> FetchRunesAsync(GameVersion? version, Language? language);

        /// <summary>
        ///     Gets a <see cref="MissionAssetData"/> from the cache. If the data is not cached it will be fetched from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is providen.
        /// </param>
        ValueTask<MissionAssetData> GetMissionAssetsAsync(GameVersion version, Language? language);

        /// <summary>
        ///     Fetches a <see cref="MapData"/> from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use. The lastest version will be fetched if <see langword="null"/> is providen.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is providen.
        /// </param>
        Task<MissionAssetData> FetchMissionAssetsAsync(GameVersion? version, Language? language = null);
    }
}