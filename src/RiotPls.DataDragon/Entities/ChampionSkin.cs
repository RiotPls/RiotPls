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

        /// <summary>
        ///     Gets the skin's splash art url of the champion. 
        ///     You can optionally indicate if you want to get the centered version.
        /// </summary>
        /// <param name="centered">
        ///     Indicates if the method should return the centered splash art url.
        /// </param>
        /// <returns></returns>
        public string GetSplashArtUrl(bool centered = false)
            => $"{Champion.Version}/champion/{Champion.Id}/splash-art/{(centered ? "centered/" : string.Empty)}skin/{SkinIndex}";

        /// <summary>
        ///     Gets the skin portrait's url of the champion.
        /// </summary>
        public string GetPortraitUrl()
            => $"{Champion.Version}/champion/{Champion.Id}/portrait/skin/{SkinIndex}";

        /// <summary>
        ///     Gets the skin tile's url of the champion.
        /// </summary>
        public string GetTileUrl()
            => $"{Champion.Version}/champion/{Champion.Id}/tile/skin/{SkinIndex}";
    }
}