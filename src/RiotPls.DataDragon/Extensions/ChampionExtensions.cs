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
        public static async Task<ChampionBase> GetChampionByIdAsync(this DataDragonClient client, 
            string id, string version, string language = "en_US")
        {
            var champions = await client.GetChampionsAsync(version, language);
            return champions.Champions[id];
        }
        
        /// <summary>
        /// Gets a champion information by its name.
        /// </summary>
        /// <param name="client">Data Dragon client that performs the request.</param>
        /// <param name="name">Name of the champion.</param>
        /// <param name="version">Version of Data Dragon.</param>
        /// <param name="language">Language in which the data must be returned.</param>
        public static async Task<ChampionBase> GetChampionByNameAsync(this DataDragonClient client, 
            string name, string version, string language = "en_US")
        {
            var champions = await client.GetChampionsAsync(version, language);
            return champions.Champions.Values.First(
                x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}