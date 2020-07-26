using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using RiotPls.DataDragon;
using RiotPls.DataDragon.Entities;
using RiotPls.DataDragon.Enums;
using RiotPls.Entities;

namespace RiotPls
{
    public class LeagueClient
    {
        public static DataDragonDataContainer? DataDragonDataContainer { get; private set; }
        public static DataDragonClient DataDragonClient { get; }

        static LeagueClient()
        {
            DataDragonClient = new DataDragonClient();
        }

        public static async Task<DataDragonDataContainer> GetLatestStaticDataAsync(CancellationToken token)
        {
            var version = await DataDragonClient.FetchLatestVersionAsync(token)
                .ConfigureAwait(false);

            var champions = DataDragonClient.GetChampionsAsync(version!, token: token)
                .ConfigureAwait(false);
            var items = await DataDragonClient.GetItemsAsync(version!, token: token)
                .ConfigureAwait(false);
            var languages = await DataDragonClient.GetLanguagesAsync(token)
                .ConfigureAwait(false);
            var maps = await DataDragonClient.GetMapsAsync(version!, token: token)
                .ConfigureAwait(false);
            var runes = await DataDragonClient.GetRunesAsync(version!, token: token)
                .ConfigureAwait(false);
            var missionAssets = await DataDragonClient.GetMissionAssetsAsync(version!, token: token)
                .ConfigureAwait(false);
            var profileIcons = await DataDragonClient.GetProfileIconsAsync(version!, token: token)
                .ConfigureAwait(false);
            var summonerSpells = await DataDragonClient.GetSummonerSpellsAsync(version!, token: token)
                .ConfigureAwait(false);

            return DataDragonDataContainer = new DataDragonDataContainer(version, champions, items, languages, maps,
                runes, missionAssets, profileIcons, summonerSpells);
        }
    }
}