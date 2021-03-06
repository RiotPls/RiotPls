﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using RiotPls.DataDragon.Entities;

namespace RiotPls.DataDragon.Converters
{
    internal sealed class GameVersionJsonConverter : JsonConverter<GameVersion>
    {
        public static GameVersionJsonConverter Instance { get; } = new GameVersionJsonConverter();

        public override GameVersion Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => GameVersion.Parse(reader.GetString());

        public override void Write(Utf8JsonWriter writer, GameVersion value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString());
    }
}