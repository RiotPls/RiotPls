using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RiotPls.DataDragon;
using RiotPls.DataDragon.Entities;
using RiotPls.DataDragon.Enums;

namespace RiotPls.Entities
{
    public sealed class DataDragonDataContainer
    {
        public GameVersion Version { get; }
        public Language Language { get; }
        public IReadOnlyList<Champion>? Champions { get; }
        public IReadOnlyCollection<Item>? Items { get; }
        public IReadOnlyList<Language>? Languages { get; }
        public IReadOnlyCollection<Map>? Maps { get; }
        public IReadOnlyList<Rune>? Runes { get; }
        public IReadOnlyCollection<MissionAsset>? MissionAssets { get; }
        public IReadOnlyCollection<ProfileIcon>? ProfileIcons { get; }
        public IReadOnlyCollection<SummonerSpell>? SummonerSpells { get; }

        public DataDragonDataContainer(GameVersion version, Language language, IReadOnlyCollection<Item>? items, IReadOnlyList<Language>? languages, IReadOnlyCollection<Map>? maps, IReadOnlyList<Rune>? runes, IReadOnlyCollection<MissionAsset>? missionAssets, IReadOnlyCollection<ProfileIcon>? profileIcons, IReadOnlyCollection<SummonerSpell>? summonerSpells)
        {
            Version = version;
            Language = language;
            Items = items;
            Languages = languages;
            Maps = maps;
            Runes = runes;
            MissionAssets = missionAssets;
            ProfileIcons = profileIcons;
            SummonerSpells = summonerSpells;
        }

        public static async Task<DataDragonDataContainer> FetchAsync(
            IDataDragonClient client,
            GameVersion version,
            Language? language = null,
            CancellationToken token = default)
        {
            var lang = language ?? client.DefaultLanguage;
            var champions = new List<Champion>(150);

            await foreach (var champion in client.GetChampionsAsync(version, lang, token).ConfigureAwait(false))
                champions.Add(champion!);

            var items = await client.GetItemsAsync(version, lang, token)
                .ConfigureAwait(false);
            var languages = await client.GetLanguagesAsync(token)
                .ConfigureAwait(false);
            var maps = await client.GetMapsAsync(version, lang, token)
                .ConfigureAwait(false);
            var runes = await client.GetRunesAsync(version, lang, token)
                .ConfigureAwait(false);
            var missionAssets = await client.GetMissionAssetsAsync(version, lang, token)
                .ConfigureAwait(false);
            var profileIcons = await client.GetProfileIconsAsync(version, lang, token)
                .ConfigureAwait(false);
            var summonerSpells = await client.GetSummonerSpellsAsync(version, lang, token)
                .ConfigureAwait(false);

            return new DataDragonDataContainer(version, lang, items, languages, maps, runes, missionAssets, profileIcons, summonerSpells);
        }
    }
}