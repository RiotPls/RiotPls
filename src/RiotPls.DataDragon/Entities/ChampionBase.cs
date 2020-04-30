using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace RiotPls.DataDragon.Entities
{
    public class ChampionData
    {
        /// <summary>
        /// Type of retrieved data.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }
        
        /// <summary>
        /// Format of this data.
        /// </summary>
        [JsonPropertyName("format")]
        public string Format { get; set; }
        
        /// <summary>
        /// Version of the data.
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; }

        /// <summary>
        /// Dictionary of champions by their id.
        /// </summary>
        public IReadOnlyDictionary<string, ChampionBase> Champions =>
            _champions ??= ChampionsRaw.ToDictionary(x => x.Value.Key, y => y.Value);
        
        [JsonPropertyName("data")]
        public IReadOnlyDictionary<string, ChampionBase> ChampionsRaw { get; set; }
        private IReadOnlyDictionary<string, ChampionBase> _champions;
    }
    
    public class ChampionBase
    {
        /// <summary>
        /// Version of the data.
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; }
        
        /// <summary>
        /// Id of the champion. Actually its name.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        /// <summary>
        /// Key of the champion. Actually its id.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }
        
        /// <summary>
        /// Name of the champion.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Title of the champion.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        /// <summary>
        /// Summary of the champion.
        /// </summary>
        [JsonPropertyName("blurb")]
        public string Blurb { get; set; }
        
        /// <summary>
        /// Partype of the champion.
        /// </summary>
        [JsonPropertyName("partype")]
        public string Partype { get; set; }
        
        /// <summary>
        /// Information about the champion. Whether it's an offensive, magic, defensive, etc. champion.
        /// </summary>
        [JsonPropertyName("info")]
        public ChampionInfo Info { get; set; }
        
        /// <summary>
        /// Information and name of the images associated with that champion.
        /// </summary>
        [JsonPropertyName("image")]
        public ChampionImage Image { get; set; }
        
        /// <summary>
        /// Tags representing the champion.
        /// </summary>
        [JsonPropertyName("tags")]
        public IReadOnlyCollection<string> Tags { get; set; }
        
        /// <summary>
        /// Stats of the champion.
        /// </summary>
        [JsonPropertyName("stats")]
        public ChampionStats Stats { get; set; } 
    }

    public class ChampionInfo
    {
        /// <summary>
        /// Attack level of the champion.
        /// </summary>
        [JsonPropertyName("attack")]
        public int Attack { get; set; }
        
        /// <summary>
        /// Defense level of the champion.
        /// </summary>
        [JsonPropertyName("defense")]
        public int Defense { get; set; }
        
        /// <summary>
        /// Magic level of the champion.
        /// </summary>
        [JsonPropertyName("magic")]
        public int Magic { get; set; }
        
        /// <summary>
        /// Difficulty level of the champion.
        /// </summary>
        [JsonPropertyName("difficulty")]
        public int Difficulty { get; set; }
    }

    public class ChampionImage
    {
        /// <summary>
        /// Name of the image for the full splash-art for the champion.
        /// </summary>
        [JsonPropertyName("full")]
        public string Full { get; set; }
        
        /// <summary>
        /// name of the image for the sprite of the champion.
        /// </summary>
        [JsonPropertyName("sprite")]
        public string Sprite { get; set; }
        
        /// <summary>
        /// Group of the champion.
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

    public class ChampionStats
    {
        /// <summary>
        /// Base HP of the champion.
        /// </summary>
        [JsonPropertyName("hp")]
        public double Hp { get; set; }
        
        /// <summary>
        /// Amount of HP per level the champion acquires. 
        /// </summary>
        [JsonPropertyName("hpperlevel")]
        public double HpPerLevel { get; set; }
        
        /// <summary>
        /// Base MP of the champion.
        /// </summary>
        [JsonPropertyName("mp")]
        public double Mp { get; set; }
        
        /// <summary>
        /// Amount of MP per level the champion acquires.
        /// </summary>
        [JsonPropertyName("mpperlevel")]
        public double MpPerLevel { get; set; }
        
        /// <summary>
        /// Base move speed of the champion.
        /// </summary>
        [JsonPropertyName("movespeed")]
        public double MoveSpeed { get; set; }
        
        /// <summary>
        /// Base armor of the champion.
        /// </summary>
        [JsonPropertyName("armor")]
        public double Armor { get; set; }
        
        /// <summary>
        /// Amount of armor per level the champion acquires.
        /// </summary>
        [JsonPropertyName("armorperlevel")]
        public double ArmorPerLevel { get; set; }
        
        /// <summary>
        /// Base spell block of the champion.
        /// </summary>
        [JsonPropertyName("spellblock")]
        public double SpellBlock { get; set; }
        
        /// <summary>
        /// Amount of spell block per level the champion acquires.
        /// </summary>
        [JsonPropertyName("spellblockperlevel")]
        public double SpellBlockPerLevel { get; set; }
        
        /// <summary>
        /// Base attack range of the champion.
        /// </summary>
        [JsonPropertyName("attackrange")]
        public double AttackRange { get; set; }
        
        /// <summary>
        /// Base HP regen of the champion.
        /// </summary>
        [JsonPropertyName("hpregen")]
        public double HpRegen { get; set; }
        
        /// <summary>
        /// Amount of HP regen per level the champion acquires.
        /// </summary>
        [JsonPropertyName("hpregenperlevel")]
        public double HpRegenPerLevel { get; set; }
        
        /// <summary>
        /// Base MP regen of the champion.
        /// </summary>
        [JsonPropertyName("mpregen")]
        public double MpRegen { get; set; }
        
        /// <summary>
        /// Amount of MP regen per level the champion acquires.
        /// </summary>
        [JsonPropertyName("mpregenperlevel")]
        public double MpRegenPerLevel { get; set; }
        
        /// <summary>
        /// Base critical damage rate of the champion.
        /// </summary>
        [JsonPropertyName("crit")]
        public double Crit { get; set; }
        
        /// <summary>
        /// Amount of critical damage rate per level the champion acquires.
        /// </summary>
        [JsonPropertyName("critperlevel")]
        public double CritPerLevel { get; set; }
        
        /// <summary>
        /// Base attack damage of the champion.
        /// </summary>
        [JsonPropertyName("attackdamage")]
        public double AttackDamage { get; set; }
        
        /// <summary>
        /// Amount of attack damage per level the champion acquires.
        /// </summary>
        [JsonPropertyName("attackdamageperlevel")]
        public double AttackDamagePerLevel { get; set; }
        
        /// <summary>
        /// Base attack speed of the champion.
        /// </summary>
        [JsonPropertyName("attackspeed")]
        public double AttackSpeed { get; set; }
        
        /// <summary>
        /// Amount of attack speed per level the champion acquires.
        /// </summary>
        [JsonPropertyName("attackspeedperlevel")]
        public double AttackSpeedPerLevel { get; set; }
    }
}