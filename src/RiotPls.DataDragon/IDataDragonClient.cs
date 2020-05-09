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
        ///     Gets a <see cref="ChampionBaseData"/> from the cache containing base information
        ///     about every champion on the game. If the data is not available it will be fetched from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language to use.
        /// </param>
        ValueTask<ChampionBaseData> GetPartialChampionsAsync(GameVersion version, Language language);

        /// <summary>
        ///     Fetches a <see cref="ChampionBaseData"/> from the API.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language to use.
        /// </param>
        Task<ChampionBaseData> FetchPartialChampionAsync(GameVersion version, Language language);

        /// <summary>
        ///    Returns a <see cref="ChampionFullData"/> containing full information
        ///    about every champion on the game.
        /// </summary>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        ValueTask<ChampionFullData> GetChampionsAsync(GameVersion version, Language language);

        /// <summary>
        ///    Returns a <see cref="ChampionData"/> containing full information
        ///    about a specific champion on the game.
        /// </summary>
        /// <param name="championName">
        ///    Name of the champion to query.
        /// </param>
        /// <param name="version">
        ///   The version of Data Dragon to use.
        /// </param>
        ValueTask<ChampionData> GetChampionAsync(ChampionId championName, GameVersion version, Language language);

        /// <summary>
        ///    Returns a <see cref="SummonerSpellData"/> containing full information
        ///    about the whole game's summoner spells
        /// </summary>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///    The language in which the data must be returned. Defaults to English (United States).
        /// </param>
        ValueTask<SummonerSpellData> GetSummonerSpellsAsync(GameVersion version, Language language);

        /// <summary>
        ///    Returns a <see cref="SummonerSpellData"/> containing full information
        ///    about the whole game's summoner spells
        /// </summary>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///    The language in which the data must be returned. Defaults to English (United States).
        /// </param>
        ValueTask<ProfileIconData> GetProfileIconsAsync(GameVersion version, Language language);

        /// <summary>
        ///    Returns a <see cref="MapData"/> containing full information
        ///    about the whole maps.
        /// </summary>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///    The language in which the data must be returned. Defaults to English (United States).
        /// </param>
        ValueTask<MapData> GetMapsAsync(GameVersion version, Language language);

        /// <summary>
        ///    Returns a set of <see cref="Rune"/> containing full information
        ///    about the different runes and their slots.
        /// </summary>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///    The language in which the data must be returned. Defaults to English (United States).
        /// </param>
        ValueTask<IReadOnlyList<Rune>> GetRunesAsync(GameVersion version, Language language);

        /// <summary>
        ///    Returns a set of <see cref="MissionAssetData"/> containing full information
        ///    about the different mission assets.
        /// </summary>
        /// <param name="version">
        ///    The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///    The language in which the data must be returned. Defaults to English (United States).
        /// </param>
        public ValueTask<MissionAssetData> GetMissionAssetsAsync(GameVersion version, Language? language = null);
    }
}