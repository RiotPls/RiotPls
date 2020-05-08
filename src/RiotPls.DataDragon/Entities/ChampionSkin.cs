namespace RiotPls.DataDragon.Entities
{
    /// <summary>
    ///     Represent a skin of a champion.
    /// </summary>
    public sealed class ChampionSkin
    {
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

        internal ChampionSkin(ChampionSkinDto dto)
        {
            Id = int.Parse(dto.Id);
            SkinIndex = dto.Num;
            Name = dto.Name;
            HasChromas = dto.HasChromas;
        }
    }
}