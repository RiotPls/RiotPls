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
        ///     Gets a <see cref="IReadOnlyList{ChampionSummary}"/> from the cache.
        ///     If the data is not cached it will be fetched from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is provided.
        /// </param>
        ValueTask<IReadOnlyList<ChampionSummary>> GetChampionsSummaryAsync(GameVersion version, Language? language);

        /// <summary>
        ///     Fetches a <see cref="IReadOnlyList{ChampionSummary}"/> from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        ///     The latest version will be fetched if <see langword="null"/> is provided.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is provided.
        /// </param>
        Task<IReadOnlyList<ChampionSummary>> FetchChampionsSummaryAsync(GameVersion? version, Language? language);

        /// <summary>
        ///     Gets a <see cref="IReadOnlyList{Champion}"/> from the cache.
        ///     If the data is not cached it will be fetched from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is provided.
        /// </param>
        ValueTask<IReadOnlyList<Champion>> GetChampionsAsync(GameVersion version, Language? language);

        /// <summary>
        ///     Fetches a <see cref="IReadOnlyList{Champion}"/> from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        ///     The latest version will be fetched if <see langword="null"/> is provided.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is provided.
        /// </param>
        Task<IReadOnlyList<Champion>> FetchChampionsAsync(GameVersion? version, Language? language);

        /// <summary>
        ///     Gets a <see cref="ChampionSummary"/> from the cache.
        ///     If the data is not cached it will be fetched from the API.
        /// </summary>
        /// <param name="championId">
        ///     The id of the Champion.
        /// </param>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is provided.
        /// </param>
        ValueTask<ChampionSummary> GetChampionSummaryAsync(ChampionId championId, GameVersion version, Language? language);

        /// <summary>
        ///     Fetches a <see cref="ChampionSummary"/> from the API.
        /// </summary>
        /// <param name="championId">
        ///     The id of the Champion.
        /// </param>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        ///     The latest version will be fetched if <see langword="null"/> is provided.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is provided.
        /// </param>
        Task<ChampionSummary> FetchChampionSummaryAsync(ChampionId championId, GameVersion? version, Language? language);

        /// <summary>
        ///     Gets a <see cref="Champion"/> from the cache.
        ///     If the data is not cached it will be fetched from the API.
        /// </summary>
        /// <param name="championId">
        ///     The id of the Champion.
        /// </param>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is provided.
        /// </param>
        ValueTask<Champion> GetChampionAsync(ChampionId championId, GameVersion version, Language? language);

        /// <summary>
        ///     Fetches a <see cref="Champion"/> from the API.
        /// </summary>
        /// <param name="championId">
        ///     The id of the Champion.
        /// </param>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        ///     The latest version will be fetched if <see langword="null"/> is provided.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is provided.
        /// </param>
        Task<Champion> FetchChampionAsync(ChampionId championId, GameVersion? version, Language? language);

        /// <summary>
        ///     Gets a <see cref="IReadOnlyCollection{SummonerSpell}"/> from the cache.
        ///     If the data is not cached it will be fetched from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is provided.
        /// </param>
        ValueTask<IReadOnlyCollection<SummonerSpell>> GetSummonerSpellsAsync(GameVersion version, Language? language);

        /// <summary>
        ///     Fetches a <see cref="IReadOnlyCollection{SummonerSpell}"/> from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        ///     The latest version will be fetched if <see langword="null"/> is provided.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is provided.
        /// </param>
        Task<IReadOnlyCollection<SummonerSpell>> FetchSummonerSpellsAsync(GameVersion? version, Language? language);

        /// <summary>
        ///     Gets a <see cref="IReadOnlyCollection{ProfileIcon}"/> from the cache.
        ///     If the data is not cached it will be fetched from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is provided.
        /// </param>
        ValueTask<IReadOnlyCollection<ProfileIcon>> GetProfileIconsAsync(GameVersion version, Language? language);

        /// <summary>
        ///     Fetches a <see cref="IReadOnlyCollection{ProfileIcon}"/> from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        ///     The latest version will be fetched if <see langword="null"/> is provided.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is provided.
        /// </param>
        Task<IReadOnlyCollection<ProfileIcon>> FetchProfileIconsAsync(GameVersion? version, Language? language);

        /// <summary>
        ///     Gets a <see cref="IReadOnlyCollection{Map}"/> from the cache.
        ///     If the data is not cached it will be fetched from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is provided.
        /// </param>
        ValueTask<IReadOnlyCollection<Map>> GetMapsAsync(GameVersion version, Language? language);

        /// <summary>
        ///     Fetches a <see cref="IReadOnlyCollection{Map}"/> from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        ///     The latest version will be fetched if <see langword="null"/> is provided.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is provided.
        /// </param>
        Task<IReadOnlyCollection<Map>> FetchMapsAsync(GameVersion? version, Language? language);

        /// <summary>
        ///     Gets a <see cref="IReadOnlyList{Rune}"/> from the cache.
        ///     If the data is not cached it will be fetched from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is provided.
        /// </param>
        ValueTask<IReadOnlyList<Rune>> GetRunesAsync(GameVersion version, Language? language);

        /// <summary>
        ///     Fetches a <see cref="IReadOnlyList{Rune}"/> from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        ///     The latest version will be fetched if <see langword="null"/> is provided.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is provided.
        /// </param>
        Task<IReadOnlyList<Rune>> FetchRunesAsync(GameVersion? version, Language? language);

        /// <summary>
        ///     Gets a <see cref="IReadOnlyCollection{MissionAsset}"/> from the cache.
        ///     If the data is not cached it will be fetched from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is provided.
        /// </param>
        ValueTask<IReadOnlyCollection<MissionAsset>> GetMissionAssetsAsync(GameVersion version, Language? language);

        /// <summary>
        ///     Fetches a <see cref="IReadOnlyCollection{MissionAsset}"/> from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        ///     The latest version will be fetched if <see langword="null"/> is provided.
        /// </param>
        /// <param name="language">
        ///     The language to use. <see cref="DefaultLanguage"/> if <see langword="null"/> is provided.
        /// </param>
        Task<IReadOnlyCollection<MissionAsset>> FetchMissionAssetsAsync(GameVersion? version, Language? language = null);
    }
}