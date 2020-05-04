using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using RiotPls.DataDragon.Entities;

namespace RiotPls.DataDragon.Converters
{
    public sealed class GameLanguageJsonConverter : JsonConverter<GameLanguage>
    {
        public static GameLanguageJsonConverter Instance { get; } = new GameLanguageJsonConverter();
        
        public override GameLanguage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => GameLanguage.Parse(reader.GetString());

        public override void Write(Utf8JsonWriter writer, GameLanguage value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString());
    }
}