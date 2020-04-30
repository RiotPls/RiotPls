using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RiotPls.DataDragon.Entities
{
    public class ChampionBase
    {
        /// <summary>
        ///     The version of the data.
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; }
        
        /// <summary>
        ///     The unique identifier of the champion.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        /// <summary>
        ///     The unique key of the champion.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }
        
        /// <summary>
        ///     The name of the champion.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        /// <summary>
        ///     The title of the champion.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        /// <summary>
        ///     The summary of the champion.
        /// </summary>
        [JsonPropertyName("blurb")]
        public string Blurb { get; set; }
        
        /// <summary>
        ///     The partype of the champion.
        /// </summary>
        [JsonPropertyName("partype")]
        public string Partype { get; set; }
        
        /// <summary>
        ///     Statistics about the champion.
        /// </summary>
        [JsonPropertyName("info")]
        public ChampionInfo Info { get; set; }
        
        /// <summary>
        ///     Information and name of the images associated with that champion.
        /// </summary>
        [JsonPropertyName("image")]
        public ChampionImage Image { get; set; }
        
        /// <summary>
        ///     Tags representing the champion.
        /// </summary>
        [JsonPropertyName("tags")]
        public IReadOnlyCollection<string> Tags { get; set; }
        
        /// <summary>
        ///     Statistics related to the champion.
        /// </summary>
        [JsonPropertyName("stats")]
        public ChampionStats Stats { get; set; } 
    }

    public class ChampionInfo
    {
        /// <summary>
        ///     The attack level of the champion.
        /// </summary>
        [JsonPropertyName("attack")]
        public int Attack { get; set; }
        
        /// <summary>
        ///     The defense level of the champion.
        /// </summary>
        [JsonPropertyName("defense")]
        public int Defense { get; set; }
        
        /// <summary>
        ///     The magic level of the champion.
        /// </summary>
        [JsonPropertyName("magic")]
        public int Magic { get; set; }
        
        /// <summary>
        ///     The difficulty level of the champion.
        /// </summary>
        [JsonPropertyName("difficulty")]
        public int Difficulty { get; set; }
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
        [JsonPropertyName("full")]
        public string Full { get; set; }
        
        /// <summary>
        ///     The name of the image for the sprite of the champion.
        /// </summary>
        [JsonPropertyName("sprite")]
        public string Sprite { get; set; }
        
        /// <summary>
        ///     The group of the champion.
        /// </summary>
        [JsonPropertyName("group")]
        public string Group { get; set; }
        
        [JsonPropertyName("x")]
        public int X { get; set; }
        
        [JsonPropertyName("y")]
        public int Y { get; set; }
        
        [JsonPropertyName("w")]
        public int W { get; set; }
        
        [JsonPropertyName("h")]
        public int H { get; set; }
    }

    /// <summary>
    ///     Represents in-game statistics for a champion, which are used for balancing.
    /// </summary>
    public class ChampionStats
    {
        /// <summary>
        ///     The base HP of the champion.
        /// </summary>
        [JsonPropertyName("hp")]
        public double Hp { get; set; }
        
        /// <summary>
        ///     The amount of HP per level the champion acquires. 
        /// </summary>
        [JsonPropertyName("hpperlevel")]
        public double HpPerLevel { get; set; }
        
        /// <summary>
        ///     The base MP of the champion.
        /// </summary>
        [JsonPropertyName("mp")]
        public double Mp { get; set; }
        
        /// <summary>
        ///     The amount of MP per level the champion acquires.
        /// </summary>
        [JsonPropertyName("mpperlevel")]
        public double MpPerLevel { get; set; }
        
        /// <summary>
        ///     The base move speed of the champion.
        /// </summary>
        [JsonPropertyName("movespeed")]
        public double MoveSpeed { get; set; }
        
        /// <summary>
        ///     The base armor of the champion.
        /// </summary>
        [JsonPropertyName("armor")]
        public double Armor { get; set; }
        
        /// <summary>
        ///     The amount of armor per level the champion acquires.
        /// </summary>
        [JsonPropertyName("armorperlevel")]
        public double ArmorPerLevel { get; set; }
        
        /// <summary>
        ///     The base spell block of the champion.
        /// </summary>
        [JsonPropertyName("spellblock")]
        public double SpellBlock { get; set; }
        
        /// <summary>
        ///     The amount of spell block per level the champion acquires.
        /// </summary>
        [JsonPropertyName("spellblockperlevel")]
        public double SpellBlockPerLevel { get; set; }
        
        /// <summary>
        ///     The base attack range of the champion.
        /// </summary>
        [JsonPropertyName("attackrange")]
        public double AttackRange { get; set; }
        
        /// <summary>
        ///     The base HP regen of the champion.
        /// </summary>
        [JsonPropertyName("hpregen")]
        public double HpRegen { get; set; }
        
        /// <summary>
        ///     The amount of HP regen per level the champion acquires.
        /// </summary>
        [JsonPropertyName("hpregenperlevel")]
        public double HpRegenPerLevel { get; set; }
        
        /// <summary>
        ///     The base MP regen of the champion.
        /// </summary>
        [JsonPropertyName("mpregen")]
        public double MpRegen { get; set; }
        
        /// <summary>
        ///     The amount of MP regen per level the champion acquires.
        /// </summary>
        [JsonPropertyName("mpregenperlevel")]
        public double MpRegenPerLevel { get; set; }
        
        /// <summary>
        ///     The base critical damage rate of the champion.
        /// </summary>
        [JsonPropertyName("crit")]
        public double Crit { get; set; }
        
        /// <summary>
        ///     The amount of critical damage rate per level the champion acquires.
        /// </summary>
        [JsonPropertyName("critperlevel")]
        public double CritPerLevel { get; set; }
        
        /// <summary>
        ///     The base attack damage of the champion.
        /// </summary>
        [JsonPropertyName("attackdamage")]
        public double AttackDamage { get; set; }
        
        /// <summary>
        ///     The amount of attack damage per level the champion acquires.
        /// </summary>
        [JsonPropertyName("attackdamageperlevel")]
        public double AttackDamagePerLevel { get; set; }
        
        /// <summary>
        ///     The base attack speed of the champion.
        /// </summary>
        [JsonPropertyName("attackspeed")]
        public double AttackSpeed { get; set; }
        
        /// <summary>
        ///     The amount of attack speed per level the champion acquires.
        /// </summary>
        [JsonPropertyName("attackspeedperlevel")]
        public double AttackSpeedPerLevel { get; set; }
    }
}