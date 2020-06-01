using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RiotPls.DataDragon.Converters
{
    internal sealed class DoubleArrayJsonConverter : JsonConverter<double[]>
    {
        public override bool CanConvert(Type typeToConvert)
            => typeToConvert == typeof(double) || typeToConvert == typeof(double[]);

        public override double[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.TokenType == JsonTokenType.StartArray
            ? JsonSerializer.Deserialize<double[]>(ref reader, options)
            : new[] { reader.GetDouble() };

        public override void Write(Utf8JsonWriter writer, double[] value, JsonSerializerOptions options)
            => JsonSerializer.Serialize(writer, value, options);
    }
}
