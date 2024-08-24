using NUlid;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jgcarmona.Qna.Common.Converters
{
    public class UlidJsonConverter : JsonConverter<Ulid>
    {
        public override Ulid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var ulidString = reader.GetString();
                if (Ulid.TryParse(ulidString, out var ulid))
                {
                    return ulid;
                }
                throw new JsonException($"Invalid ULID format: {ulidString}");
            }
            throw new JsonException($"Unexpected token parsing ULID. Expected String, got {reader.TokenType}.");
        }

        public override void Write(Utf8JsonWriter writer, Ulid value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
