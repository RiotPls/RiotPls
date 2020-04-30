using System.Collections.Generic;

namespace RiotPls.DataDragon.Entities
{
    public class ChampionBase
    {
        /// <summary>
        ///     The version of the data.
        /// </summary>
        public GameVersion Version { get; }
        
        /// <summary>
        ///     The unique identifier of the champion.
        /// </summary>
        public string Id { get; }
        
        /// <summary>
        ///     The unique key of the champion.
        /// </summary>
        public string Key { get; }
        
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
        public string Blurb { get; }
        
        /// <summary>
        ///     The partype of the champion.
        /// </summary>
        public string Partype { get; }
        
        /// <summary>
        ///     Statistics about the champion.
        /// </summary>
        public ChampionInfo Info { get; }
        
        /// <summary>
        ///     Information and name of the images associated with that champion.
        /// </summary>
        public ChampionImage Image { get; }
        
        /// <summary>
        ///     Tags representing the champion.
        /// </summary>
        public IReadOnlyCollection<string> Tags { get; }
        
        /// <summary>
        ///     Statistics related to the champion.
        /// </summary>
        public ChampionStats Stats { get; }

        internal ChampionBase(ChampionBaseDto dto)
        {
            Version = dto.Version;
            Id = dto.Id;
            Key = dto.Key;
            Name = dto.Name;
            Title = dto.Title;
            Blurb = dto.Blurb;
            Partype = dto.Partype;
            Info = new ChampionInfo(dto.Info);
            Image = new ChampionImage(dto.Image);
            Tags = dto.Tags;
            Stats = new ChampionStats(dto.Stats);
        }
    }

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

    /// <summary>
    ///     Represents information about images and splash art for a champion.
    /// </summary>
    public class ChampionImage
    {
        // TODO: Implement URL helpers for champion images.

        /// <summary>
        ///     The name of the image for the full splash art for the champion.
        /// </summary>
        public string Full { get; }
        
        /// <summary>
        ///     The name of the image for the sprite of the champion.
        /// </summary>
        public string Sprite { get; }
        
        /// <summary>
        ///     The group of the champion.
        /// </summary>
        public string Group { get; }
        
        public int X { get; }
        
        public int Y { get; }
        
        public int W { get; }
        
        public int H { get; }

        internal ChampionImage(ChampionImageDto dto)
        {
            Full = dto.Full;
            Sprite = dto.Sprite;
            Group = dto.Group;
            X = dto.X;
            Y = dto.Y;
            W = dto.W;
            H = dto.H;
        }
    }

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
        public double MoveSpeed { get; }
        
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
        public double SpellBlock { get; }
        
        /// <summary>
        ///     The amount of spell block per level the champion acquires.
        /// </summary>
        public double SpellBlockPerLevel { get; }
        
        /// <summary>
        ///     The base attack range of the champion.
        /// </summary>
        public double AttackRange { get; }
        
        /// <summary>
        ///     The base HP regen of the champion.
        /// </summary>
        public double HpRegen { get; }
        
        /// <summary>
        ///     The amount of HP regen per level the champion acquires.
        /// </summary>
        public double HpRegenPerLevel { get; }
        
        /// <summary>
        ///     The base MP regen of the champion.
        /// </summary>
        public double MpRegen { get; }
        
        /// <summary>
        ///     The amount of MP regen per level the champion acquires.
        /// </summary>
        public double MpRegenPerLevel { get; }
        
        /// <summary>
        ///     The base critical damage rate of the champion.
        /// </summary>
        public double Crit { get; }
        
        /// <summary>
        ///     The amount of critical damage rate per level the champion acquires.
        /// </summary>
        public double CritPerLevel { get; }
        
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
            HpRegen = dto.HpRegen;
            HpPerLevel = dto.HpPerLevel;
            HpRegenPerLevel = dto.HpRegenPerLevel;
            Mp = dto.Mp;
            MpRegen = dto.MpRegen;
            MpPerLevel = dto.MpPerLevel;
            MpRegenPerLevel = dto.MpRegenPerLevel;
            MoveSpeed = dto.MoveSpeed;
            Armor = dto.Armor;
            ArmorPerLevel = dto.ArmorPerLevel;
            SpellBlock = dto.SpellBlock;
            SpellBlockPerLevel = dto.SpellBlockPerLevel;
            AttackRange = dto.AttackRange;
            Crit = dto.Crit;
            CritPerLevel = dto.CritPerLevel;
            AttackDamage = dto.AttackDamage;
            AttackDamagePerLevel = dto.AttackDamagePerLevel;
            AttackSpeed = dto.AttackSpeed;
            AttackSpeedPerLevel = dto.AttackSpeedPerLevel;
        }
    }
}