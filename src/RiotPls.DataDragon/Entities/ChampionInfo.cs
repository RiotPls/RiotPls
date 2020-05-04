namespace RiotPls.DataDragon.Entities
{
    /// <summary>
    ///     Represents the different levels of the champion.
    ///     Whether it's tank, attack, difficult, etc.
    /// </summary>
    public sealed class ChampionInfo
    {
        private readonly DataDragonClient _client;

        /// <summary>
        ///     The attack level of the champion.
        /// </summary>
        public int AttackLevel { get; }
        
        /// <summary>
        ///     The defense level of the champion.
        /// </summary>
        public int DefenseLevel { get; }
        
        /// <summary>
        ///     The magic level of the champion.
        /// </summary>
        public int MagicLevel { get; }
        
        /// <summary>
        ///     The difficulty level of the champion.
        /// </summary>
        public int Difficulty { get; }

        internal ChampionInfo(DataDragonClient client, ChampionInfoDto dto)
        {
            _client = client;
            
            AttackLevel = dto.Attack;
            DefenseLevel = dto.Defense;
            MagicLevel = dto.Magic;
            Difficulty = dto.Difficulty;
        }
    }
}