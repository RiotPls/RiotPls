using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using RiotPls.DataDragon.Entities;

namespace RiotPls.DataDragon.Converters
{
    public sealed class GameVersionConverter : JsonConverter<GameVersion>
    {
        public override GameVersion Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => GameVersion.Parse(reader.GetString());

        public override void Write(Utf8JsonWriter writer, GameVersion value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString());
    }
}