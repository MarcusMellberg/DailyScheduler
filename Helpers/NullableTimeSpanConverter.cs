using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DailyScheduler.Helpers
{
    public class NullableTimeSpanConverter : JsonConverter<TimeSpan?>
    {
        public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                return TimeSpan.Parse(reader.GetString());
            }

            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            throw new JsonException("Unexpected token type for TimeSpan? conversion.");
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteStringValue(value.Value.ToString(@"hh\:mm\:ss"));
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}
