namespace RiotPls.DataDragon.Entities
{
    /// <summary>
    ///     Represent a skin of a champion.
    /// </summary>
    public class ChampionSkin
    {
        /// <summary>
        ///     Id of the skin.
        /// </summary>
        public int Id { get; }
        
        /// <summary>
        ///     Index of the skin. Counts chromatics.
        /// </summary>
        public int Num { get; }

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
            Id = dto.Id;
            Num = dto.Num;
            Name = dto.Name;
            HasChromas = dto.HasChromas;
        }
    }
}