using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Threading.Tasks;
using RiotPls.DataDragon.Enums;
using RiotPls.DataDragon.Extensions;

namespace RiotPls.DataDragon.Entities
{
    public sealed class Champion
    {
        /// <summary>
        ///     The version of the data.
        /// </summary>
        public GameVersion Version { get; }

        /// <summary>
        ///     The unique identifier of the champion.
        /// </summary>
        public ChampionId Id { get; }

        /// <summary>
        ///     The unique key of the champion.
        /// </summary>
        public int Key { get; }

        /// <summary>
        ///     The name of the champion.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     The title of the champion.
        /// </summary>
        public string Title { get; }

        /// <summary>
        ///     The summary of the champion.
        /// </summary>
        public string Summary { get; }

        /// <summary>
        ///     The resource type of the champion. (mana, energy, fury)
        /// </summary>
        public ResourceType ResourceType { get; }

        /// <summary>
        ///     General statistics about the champion. 
        ///     Such a power and difficulty.
        /// </summary>
        public ChampionInformation Information { get; }

        /// <summary>
        ///     Information and name of the images associated with that champion.
        /// </summary>
        public ImageInformation Image { get; }

        /// <summary>
        ///     Tags representing the champion.
        /// </summary>
        public ChampionType Tags { get; }

        /// <summary>
        ///     Statistics related to the champion.
        /// </summary>
        public ChampionStatistics Statistics { get; }

        /// <summary>
        ///     Lore of the champion.
        /// </summary>
        public string Lore { get; }

        /// <summary>
        ///     Skins of the champion.
        /// </summary>
        public IReadOnlyList<ChampionSkin> Skins { get; }

        /// <summary>
        ///     Ally tips of the champion.
        /// </summary>
        public IReadOnlyList<string> AllyTips { get; }

        /// <summary>
        ///     Enemy tips of the champion.
        /// </summary>
        public IReadOnlyList<string> EnemyTips { get; }

        /// <summary>
        ///     Spells of the champion.
        /// </summary>
        public IReadOnlyList<Spell> Spells { get; }

        /// <summary>
        ///     Passive spell of the champion.
        /// </summary>
        public SpellBase PassiveSpell { get; }

        /// <summary>
        ///     Recommendations by their game mode.
        /// </summary>
        public IReadOnlyList<Recommendation> Recommendations { get; }

        internal Champion(ChampionDto dto, GameVersion version)
        {
            Version = version;
            Id = Enum.Parse<ChampionId>(dto.Id, true);
            Key = int.Parse(dto.Key);
            Name = dto.Name;
            Title = dto.Title;
            Summary = dto.Blurb;
            ResourceType = Enum.Parse<ResourceType>(dto.Partype.Replace(" ", string.Empty), true);
            Information = new ChampionInformation(dto.Info);
            Image = new ImageInformation(dto.Image);
            Tags = dto.Tags;
            Statistics = new ChampionStatistics(dto.Stats);
            Lore = dto.Lore;
            Skins = Array.ConvertAll(dto.Skins, Converter).ToImmutableArray();
            AllyTips = dto.AllyTips.ToImmutableArray();
            EnemyTips = dto.EnemyTips.ToImmutableArray();
            Spells = Array.ConvertAll(dto.Spells, x => new Spell(x)).ToImmutableArray();
            PassiveSpell = new SpellBase(dto.Passive);
            Recommendations = Array.ConvertAll(dto.Recommendations, x => new Recommendation(x)).ToImmutableArray();
        }

        /// <summary>
        ///     Gets the icon's url of the specified ability.
        /// </summary>
        /// <param name="ability">
        ///     The ability to get.
        /// </param>
        public string GetAbilityIconUrl(ChampionAbility ability)
            => $"{Version}/champion/{Id}/ability-icon/{ability.ToLower()}";

        /// <summary>
        ///     Gets the default splash art's url of the champion. 
        ///     You can optionally indicate if you want to get the centered version.
        /// </summary>
        /// <param name="centered">
        ///     Indicates if the method should return the centered splash art url.
        /// </param>
        public string GetSplashArtUrl(bool centered = false)
            => $"{Version}/champion/{Id}/splash-art{(centered ? "/centered" : string.Empty)}";

        /// <summary>
        ///     Gets the default portrait's url of the champion.
        /// </summary>
        public string GetPortraitUrl()
            => $"{Version}/champion/{Id}/portrait";

        /// <summary>
        ///     Gets the champion's square asset url.
        /// </summary>
        public string GetSquareUrl()
            => $"{Version}/champion/{Id}/square";

        /// <summary>
        ///     Gets the default tile's url of the champion.
        /// </summary>
        public string GetTileUrl()
            => $"{Version}/champion/{Id}/tile";

        /// <summary>
        ///     Gets the sound's url the champion emits when it's chosen.
        /// </summary>
        public string GetChooseSoundUrl()
            => $"{Version}/champion/{Id}/champ-select/sounds/choose";
        
        /// <summary>
        ///     Gets the sound's url the champion emits when it's banned.
        /// </summary>
        public string GetBanSoundUrl()
            => $"{Version}/champion/{Id}/champ-select/sounds/ban";

        /// <summary>
        ///     Gets the champion-selection's url special sound effects of the champion.
        /// </summary>
        public string GetSpecialSoundEffectUrl()
            => $"{Version}/champion/{Id}/champ-select/sounds/sfx";

        private ChampionSkin Converter(ChampionSkinDto dto)
            => new ChampionSkin(this, dto);
    }
}