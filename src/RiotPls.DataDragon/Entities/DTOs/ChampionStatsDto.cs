using System.Text.Json.Serialization;

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
}