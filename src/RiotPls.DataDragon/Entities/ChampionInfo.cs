namespace RiotPls.DataDragon.Entities
{
    /// <summary>
    ///     Represents the different levels of the champion.
    ///     Whether it's tank, attack, difficult, etc.
    /// </summary>
    public class ChampionInfo
    {
        /// <summary>
        ///     The attack level of the champion.
        /// </summary>
        public int Attack { get; }
        
        /// <summary>
        ///     The defense level of the champion.
        /// </summary>
        public int Defense { get; }
        
        /// <summary>
        ///     The magic level of the champion.
        /// </summary>
        public int Magic { get; }
        
        /// <summary>
        ///     The difficulty level of the champion.
        /// </summary>
        public int Difficulty { get; }

        internal ChampionInfo(ChampionInfoDto dto)
        {
            Attack = dto.Attack;
            Defense = dto.Defense;
            Magic = dto.Magic;
            Difficulty = dto.Difficulty;
        }
    }
}