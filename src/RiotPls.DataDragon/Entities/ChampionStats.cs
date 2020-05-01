namespace RiotPls.DataDragon.Entities
{
    /// <summary>
    ///     Represents in-game statistics for a champion, which are used for balancing.
    /// </summary>
    public class ChampionStats
    {
        /// <summary>
        ///     The base HP of the champion.
        /// </summary>
        public double Hp { get; }
        
        /// <summary>
        ///     The amount of HP per level the champion acquires. 
        /// </summary>
        public double HpPerLevel { get; }
        
        /// <summary>
        ///     The base MP of the champion.
        /// </summary>
        public double Mp { get; }
        
        /// <summary>
        ///     The amount of MP per level the champion acquires.
        /// </summary>
        public double MpPerLevel { get; }
        
        /// <summary>
        ///     The base move speed of the champion.
        /// </summary>
        public double MovementSpeed { get; }
        
        /// <summary>
        ///     The base armor of the champion.
        /// </summary>
        public double Armor { get; }
        
        /// <summary>
        ///     The amount of armor per level the champion acquires.
        /// </summary>
        public double ArmorPerLevel { get; }
        
        /// <summary>
        ///     The base spell block of the champion.
        /// </summary>
        public double MagicResistance { get; }
        
        /// <summary>
        ///     The amount of spell block per level the champion acquires.
        /// </summary>
        public double MagicResistancePerLevel { get; }
        
        /// <summary>
        ///     The base attack range of the champion.
        /// </summary>
        public double AttackRange { get; }
        
        /// <summary>
        ///     The base HP regen of the champion.
        /// </summary>
        public double HpRegeneration { get; }
        
        /// <summary>
        ///     The amount of HP regen per level the champion acquires.
        /// </summary>
        public double HpRegenerationPerLevel { get; }
        
        /// <summary>
        ///     The base MP regen of the champion.
        /// </summary>
        public double MpRegeneration { get; }
        
        /// <summary>
        ///     The amount of MP regen per level the champion acquires.
        /// </summary>
        public double MpRegenerationPerLevel { get; }
        
        /// <summary>
        ///     The base critical damage rate of the champion.
        /// </summary>
        public double CriticalRate { get; }
        
        /// <summary>
        ///     The amount of critical damage rate per level the champion acquires.
        /// </summary>
        public double CriticalRatePerLevel { get; }
        
        /// <summary>
        ///     The base attack damage of the champion.
        /// </summary>
        public double AttackDamage { get; }
        
        /// <summary>
        ///     The amount of attack damage per level the champion acquires.
        /// </summary>
        public double AttackDamagePerLevel { get; }
        
        /// <summary>
        ///     The base attack speed of the champion.
        /// </summary>
        public double AttackSpeed { get; }
        
        /// <summary>
        ///     The amount of attack speed per level the champion acquires.
        /// </summary>
        public double AttackSpeedPerLevel { get; }

        internal ChampionStats(ChampionStatsDto dto)
        {
            Hp = dto.Hp; 
            HpRegeneration = dto.HpRegen;
            HpPerLevel = dto.HpPerLevel;
            HpRegenerationPerLevel = dto.HpRegenPerLevel;
            Mp = dto.Mp;
            MpRegeneration = dto.MpRegen;
            MpPerLevel = dto.MpPerLevel;
            MpRegenerationPerLevel = dto.MpRegenPerLevel;
            MovementSpeed = dto.MoveSpeed;
            Armor = dto.Armor;
            ArmorPerLevel = dto.ArmorPerLevel;
            MagicResistance = dto.SpellBlock;
            MagicResistancePerLevel = dto.SpellBlockPerLevel;
            AttackRange = dto.AttackRange;
            CriticalRate = dto.Crit;
            CriticalRatePerLevel = dto.CritPerLevel;
            AttackDamage = dto.AttackDamage;
            AttackDamagePerLevel = dto.AttackDamagePerLevel;
            AttackSpeed = dto.AttackSpeed;
            AttackSpeedPerLevel = dto.AttackSpeedPerLevel;
        }
    }
}