using System;
using System.Linq;
using System.Threading.Tasks;
using RiotPls.DataDragon.Entities;

namespace RiotPls.DataDragon.Extensions
{
    public static class ChampionExtensions
    {
        /// <summary>
        /// Gets a champion information by its id.
        /// </summary>
        /// <param name="client">Data Dragon client that performs the request.</param>
        /// <param name="id">Id of the champion.</param>
        /// <param name="version">Version of Data Dragon.</param>
        /// <param name="language">Language in which the data must be returned.</param>
        public static async ValueTask<ChampionBase?> GetChampionByIdAsync(this DataDragonClient client, 
            string id, GameVersion version, string language = DataDragonClient.DefaultLanguage)
        {
            var champions = await client.GetChampionsAsync(version, language).ConfigureAwait(false);
            return champions?.Champions[id];
        }
        
        /// <summary>
        /// Gets a champion information by its name.
        /// </summary>
        /// <param name="client">Data Dragon client that performs the request.</param>
        /// <param name="name">Name of the champion.</param>
        /// <param name="version">Version of Data Dragon.</param>
        /// <param name="language">Language in which the data must be returned.</param>
        public static async ValueTask<ChampionBase?> GetChampionByNameAsync(this DataDragonClient client, 
            string name, GameVersion version, string language = DataDragonClient.DefaultLanguage)
        {
            var champions = await client.GetChampionsAsync(version, language).ConfigureAwait(false);
            return champions?.Champions.Values.FirstOrDefault(
                x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}