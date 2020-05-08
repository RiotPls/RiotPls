using System;
using System.Text.Json.Serialization;
using RiotPls.DataDragon.Converters;

namespace RiotPls.DataDragon.Enums
{
    [Flags]
    [JsonConverter(typeof(ChampionTypeJsonConverter))]
    public enum ChampionType
    {
        Assassin = 0x1,
        Fighter = 0x2,
        Mage = 0x4,
        Marksman = 0x8,
        Support = 0x10,
        Tank = 0x20
    }
}