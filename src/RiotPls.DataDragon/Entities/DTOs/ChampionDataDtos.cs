using System.Collections.Generic;
using System.Text.Json.Serialization;
using RiotPls.DataDragon.Converters;

namespace RiotPls.DataDragon.Entities
{
    internal class ChampionStatsDto
    {
        [JsonPropertyName("hp")]
        public double Hp { get; set; }

        [JsonPropertyName("hpperlevel")]
        public double HpPerLevel { get; set; }

        [JsonPropertyName("mp")]
        public double Mp { get; set; }

        [JsonPropertyName("mpperlevel")]
        public double MpPerLevel { get; set; }

        [JsonPropertyName("movespeed")]
        public double MoveSpeed { get; set; }

        [JsonPropertyName("armor")]
        public double Armor { get; set; }

        [JsonPropertyName("armorperlevel")]
        public double ArmorPerLevel { get; set; }

        [JsonPropertyName("spellblock")]
        public double SpellBlock { get; set; }

        [JsonPropertyName("spellblockperlevel")]
        public double SpellBlockPerLevel { get; set; }

        [JsonPropertyName("attackrange")]
        public double AttackRange { get; set; }

        [JsonPropertyName("hpregen")]
        public double HpRegen { get; set; }

        [JsonPropertyName("hpregenperlevel")]
        public double HpRegenPerLevel { get; set; }

        [JsonPropertyName("mpregen")]
        public double MpRegen { get; set; }
        
        [JsonPropertyName("mpregenperlevel")]
        public double MpRegenPerLevel { get; set; }

        [JsonPropertyName("crit")]
        public double Crit { get; set; }

        [JsonPropertyName("critperlevel")]
        public double CritPerLevel { get; set; }

        [JsonPropertyName("attackdamage")]
        public double AttackDamage { get; set; }

        [JsonPropertyName("attackdamageperlevel")]
        public double AttackDamagePerLevel { get; set; }

        [JsonPropertyName("attackspeed")]
        public double AttackSpeed { get; set; }

        [JsonPropertyName("attackspeedperlevel")]
        public double AttackSpeedPerLevel { get; set; }
    }
    
    internal class ChampionImageDto
    {
        [JsonPropertyName("full")]
        public string Full { get; set; }

        [JsonPropertyName("sprite")]
        public string Sprite { get; set; }

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

    internal class ChampionInfoDto
    {
        [JsonPropertyName("attack")]
        public int Attack { get; set; }

        [JsonPropertyName("defense")]
        public int Defense { get; set; }

        [JsonPropertyName("magic")]
        public int Magic { get; set; }

        [JsonPropertyName("difficulty")]
        public int Difficulty { get; set; }
    }
    
    internal class ChampionBaseDto
    {
        [JsonPropertyName("version")]
        [JsonConverter(typeof(GameVersionConverter))]
        public GameVersion Version { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("blurb")]
        public string Blurb { get; set; }

        [JsonPropertyName("partype")]
        public string Partype { get; set; }

        [JsonPropertyName("info")]
        public ChampionInfoDto Info { get; set; }

        [JsonPropertyName("image")]
        public ChampionImageDto Image { get; set; }
        
        [JsonPropertyName("tags")]
        public IReadOnlyCollection<string> Tags { get; set; }

        [JsonPropertyName("stats")]
        public ChampionStatsDto Stats { get; set; } 
    }
    
    internal class ChampionDataDto
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; }
        
        [JsonPropertyName("version")]
        [JsonConverter(typeof(GameVersionConverter))]
        public GameVersion Version { get; set; }

        [JsonPropertyName("data")]
        public IReadOnlyDictionary<string, ChampionBaseDto> Champions { get; set; }
    }
}