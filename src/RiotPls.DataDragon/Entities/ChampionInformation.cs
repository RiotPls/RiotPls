namespace RiotPls.DataDragon.Entities
{
    /// <summary>
    ///     Represents the basic information of a champion,
    ///     such as power, toughness and difficulty.
    /// </summary>
    public sealed class ChampionInformation
    {
        /// <summary>
        ///     The physical power level of the champion. 
        /// </summary>
        public int PhysicalPower { get; }

        /// <summary>
        ///     The defense power level of the champion. (Toughness)
        /// </summary>
        public int DefensePower { get; }

        /// <summary>
        ///     The magic power level of the champion.
        /// </summary>
        public int MagicPower { get; }

        /// <summary>
        ///     The difficulty of the champion.
        /// </summary>
        public int Difficulty { get; }

        internal ChampionInformation(ChampionInfoDto dto)
        {
            PhysicalPower = dto.Attack;
            DefensePower = dto.Defense;
            MagicPower = dto.Magic;
            Difficulty = dto.Difficulty;
        }
    }
}