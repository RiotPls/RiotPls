using System;
using System.Threading.Tasks;
using RiotPls.DataDragon.Entities;

namespace RiotPls.DataDragon.Extensions
{
    public static class ChampionExtensions
    {
        /// <summary>
        ///    Gets a champion base information by its id.
        /// </summary>
        /// <param name="client">
        ///    Data Dragon client that performs the request.
        /// </param>
        /// <param name="id">
        ///    Id of the champion.
        /// </param>
        /// <param name="version">
        ///    Version of Data Dragon.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///    Thrown when <paramref name="client"/> is <see langword="null"/>.
        /// </exception>
        public static ValueTask<ChampionBase?> GetChampionAsync(this IDataDragonClient client,
            int id, GameVersion version)
            => client.GetChampionAsync(id, version, client.DefaultLanguage);

        /// <summary>
        ///    Gets a champion base information by its id.
        /// </summary>
        /// <param name="client">
        ///    Data Dragon client that performs the request.
        /// </param>
        /// <param name="id">
        ///    Id of the champion.
        /// </param>
        /// <param name="version">
        ///    Version of Data Dragon.
        /// </param>
        /// <param name="language">
        ///    Language in which the data must be returned.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///    Thrown when <paramref name="client"/> is <see langword="null"/>.
        /// </exception>
        public static async ValueTask<ChampionBase?> GetChampionAsync(this IDataDragonClient client,
            int id, GameVersion version, GameLanguage language)
        {
            if (client is null)
                throw new ArgumentNullException(nameof(client));

            var champions = await client.GetPartialChampionsAsync(version, language).ConfigureAwait(false);

            champions.Champions.TryGetValue(id, out var champion);
            return champion;
        }
    }
}