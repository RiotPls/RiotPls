using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using RiotPls.DataDragon.Enums;

namespace RiotPls.DataDragon.Converters
{
    public sealed class ChampionTypeJsonConverter : JsonConverter<ChampionType>
    {
        private static readonly ChampionType[] _values = (ChampionType[])Enum.GetValues(typeof(ChampionType));
        public override ChampionType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var result = default(ChampionType);

            if (reader.TokenType == JsonTokenType.StartArray)
            {
                reader.Read();

                while (reader.TokenType != JsonTokenType.EndArray)
                {
                    result |= Enum.Parse<ChampionType>(reader.GetString());
                    reader.Read();
                }
            }
            else
                throw new Exception($"Api breaking change: Expected to deserialize an array but got: {reader.TokenType}");

            return result;
        }
        public override void Write(Utf8JsonWriter writer, ChampionType value, JsonSerializerOptions options)
        {
            Span<ChampionType> span = stackalloc ChampionType[_values.Length];
            var count = 0;

            foreach (var item in _values)
            {
                if ((value & item) == item)
                    span[count++] = item;
            }

            writer.WriteStartArray();

            foreach (var item in span[..count])
                writer.WriteStringValue(item.ToString());

            writer.WriteEndArray();
        }
    }
}
