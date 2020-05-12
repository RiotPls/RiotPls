using System.IO;
using System.Threading.Tasks;

namespace RiotPls.DataDragon.Entities
{
    /// <summary>
    ///     Represent a skin of a champion.
    /// </summary>
    public sealed class ChampionSkin
    {
        /// <summary>
        ///     The champion this skin belongs to.
        /// </summary>
        public Champion Champion { get; }
        /// <summary>
        ///     Id of the skin.
        /// </summary>
        public int Id { get; }

        /// <summary>
        ///     Index of the skin. Counts chromatics.
        /// </summary>
        public int SkinIndex { get; }

        /// <summary>
        ///     Name of the skin.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Whether the skin has chromatics.
        /// </summary>
        public bool HasChromas { get; }

        internal ChampionSkin(Champion champion, ChampionSkinDto dto)
        {
            Champion = champion;
            Id = int.Parse(dto.Id);
            SkinIndex = dto.Num;
            Name = dto.Name;
            HasChromas = dto.HasChromas;
        }

        public Task<Stream> DownloadSplashArtAsync(bool centered = false)
            => DataDragonClient.CommunityDragonHttpClient.GetStreamAsync(
                $"{Champion.Version}/champion/{Champion.Id}/splash-art/{(centered ? "centered/" : string.Empty)}skin/{SkinIndex}");

        public Task<Stream> DownloadPortraitAsync()
            => DataDragonClient.CommunityDragonHttpClient.GetStreamAsync(
                $"{Champion.Version}/champion/{Champion.Id}/portrait/skin/{SkinIndex}");

        public Task<Stream> DownloadTileAsync()
            => DataDragonClient.CommunityDragonHttpClient.GetStreamAsync(
                $"{Champion.Version}/champion/{Champion.Id}/tile/skin/{SkinIndex}");            
    }
}