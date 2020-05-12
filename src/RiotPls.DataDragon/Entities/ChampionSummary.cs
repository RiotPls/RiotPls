using System;
using System.IO;
using System.Threading.Tasks;
using RiotPls.DataDragon.Enums;
using RiotPls.DataDragon.Extensions;

namespace RiotPls.DataDragon.Entities
{
    public class ChampionSummary
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
        public StaticImage Image { get; }

        /// <summary>
        ///     Tags representing the champion.
        /// </summary>
        public ChampionType Tags { get; }

        /// <summary>
        ///     Statistics related to the champion.
        /// </summary>
        public ChampionStatistics Statistics { get; }

        internal ChampionSummary(ChampionSummaryDto dto)
        {
            Version = dto.Version;
            Id = Enum.Parse<ChampionId>(dto.Id, true);
            Key = int.Parse(dto.Key);
            Name = dto.Name;
            Title = dto.Title;
            Summary = dto.Blurb;
            ResourceType = Enum.Parse<ResourceType>(dto.Partype.Replace(" ", string.Empty), true);
            Information = new ChampionInformation(dto.Info);
            Image = new StaticImage(dto.Image);
            Tags = dto.Tags;
            Statistics = new ChampionStatistics(dto.Stats);
        }

        /// <summary>
        ///     Downloads the icon of the specificed ability.
        /// </summary>
        /// <param name="ability">
        ///     The ability to download.
        /// </param>
        public Task<Stream> DownloadAbilityIconAsync(ChampionAbility ability)
            => DataDragonClient.CommunityDragonHttpClient.GetStreamAsync(
                $"{Version}/champion/{Id}/ability-icon/{ability.ToLower()}");

        /// <summary>
        ///     Downloads the default splash art of the champion. 
        ///     You can optionally indicate if you want to download the centered version.
        /// </summary>
        /// <param name="centered">
        ///     Indicates if the method should fetch the centered splash art.
        /// </param>
        public Task<Stream> DownloadSplashArtAsync(bool centered = false)
            => DataDragonClient.CommunityDragonHttpClient.GetStreamAsync(
                $"{Version}/champion/{Id}/splash-art/{(centered ? "centered" : string.Empty)}");

        /// <summary>
        ///     Downloads the default portrait of the champion.
        /// </summary>
        public Task<Stream> DownloadPortraitAsync()
            => DataDragonClient.CommunityDragonHttpClient.GetStreamAsync(
                $"{Version}/champion/{Id}/portrait");

        /// <summary>
        ///     Downloads the champion's square asset.
        /// </summary>
        public Task<Stream> DownloadSquareAsync()
            => DataDragonClient.CommunityDragonHttpClient.GetStreamAsync(
                $"{Version}/champion/{Id}/square");

        /// <summary>
        ///     Downloads the default tile of the champion.
        /// </summary>
        public Task<Stream> DownloadTileAsync()
            => DataDragonClient.CommunityDragonHttpClient.GetStreamAsync(
                $"{Version}/champion/{Id}/tile");

        /// <summary>
        ///     Downloads the sound the champion emits when it's choosen.
        /// </summary>
        public Task<Stream> DownloadChooseSoundAsync()
            => DataDragonClient.CommunityDragonHttpClient.GetStreamAsync(
                $"{Version}/champion/{Id}/champ-select/sounds/choose");


        /// <summary>
        ///     Downloads the sound the champion emits when it's banned.
        /// </summary>
        public Task<Stream> DownloadBanSoundAsync()
            => DataDragonClient.CommunityDragonHttpClient.GetStreamAsync(
                $"{Version}/champion/{Id}/champ-select/sounds/ban");

        /// <summary>
        ///     Downloads the champion-selection special sound effects of the champion.
        /// </summary>
        public Task<Stream> DownloadSpecialSoundEffectAsync()
            => DataDragonClient.CommunityDragonHttpClient.GetStreamAsync(
                $"{Version}/champion/{Id}/champ-select/sounds/sfx");
    }
}