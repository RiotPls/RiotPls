using System.Collections.Generic;
using System.Runtime.CompilerServices;
using RiotPls.DataDragon.Entities;
using RiotPls.DataDragon.Enums;

namespace RiotPls.Entities
{
    public class DataDragonDataContainer
    {
        public GameVersion? Version { get; }
        public ConfiguredCancelableAsyncEnumerable<Champion> Champions { get; }
        public IReadOnlyCollection<Item>? Items { get; }
        public IReadOnlyList<Language>? Languages { get; }
        public IReadOnlyCollection<Map>? Maps { get; }
        public IReadOnlyList<Rune>? Runes { get; }
        public IReadOnlyCollection<MissionAsset>? MissionAssets { get; }
        public IReadOnlyCollection<ProfileIcon>? ProfileIcons { get; }
        public IReadOnlyCollection<SummonerSpell>? SummonerSpells { get; }

        public DataDragonDataContainer(
            GameVersion? version, 
            in ConfiguredCancelableAsyncEnumerable<Champion> champions, 
            IReadOnlyCollection<Item>? items, 
            IReadOnlyList<Language>? languages, 
            IReadOnlyCollection<Map>? maps,
            IReadOnlyList<Rune>? runes, 
            IReadOnlyCollection<MissionAsset>? missionAssets, 
            IReadOnlyCollection<ProfileIcon>? profileIcons, 
            IReadOnlyCollection<SummonerSpell>? summonerSpells)
        {
            Version = version;
            Champions = champions;
            Items = items;
            Languages = languages;
            Maps = maps;
            Runes = runes;
            MissionAssets = missionAssets;
            ProfileIcons = profileIcons;
            SummonerSpells = summonerSpells;
        }
    }
}