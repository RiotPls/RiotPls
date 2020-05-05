﻿using System.Threading.Tasks;
using RiotPls.DataDragon.Entities;

namespace RiotPls.DataDragon.Extensions
{
    public static class ChampionExtensions
    {
        /// <summary>
        ///     Gets a champion base information by its id.
        /// </summary>
        /// <param name="client">
        ///     Data Dragon client that performs the request.
        /// </param>
        /// <param name="id">
        ///     Id of the champion.
        /// </param>
        /// <param name="version">
        ///     Version of Data Dragon.
        /// </param>
        public static ValueTask<ChampionBase?> GetChampionByIdAsync(this DataDragonClient client,
            int id, GameVersion version)
            => GetChampionByIdAsync(client, id, version, client.DefaultLanguage);
        
        /// <summary>
        ///     Gets a champion base information by its id.
        /// </summary>
        /// <param name="client">
        ///     Data Dragon client that performs the request.
        /// </param>
        /// <param name="id">
        ///     Id of the champion.
        /// </param>
        /// <param name="version">
        ///     Version of Data Dragon.
        /// </param>
        /// <param name="language">
        ///     Language in which the data must be returned.
        /// </param>
        public static async ValueTask<ChampionBase?> GetChampionByIdAsync(this DataDragonClient client, 
            int id, GameVersion version, GameLanguage language)
        {
            var champions = await client.GetPartialChampionsAsync(version, language).ConfigureAwait(false);

            champions.Champions.TryGetValue(id, out var champion);
            return champion;
        }
    }
}