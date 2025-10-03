using System.Text.Json;
using System.Text.Json.Serialization;

namespace Masterov.Domain.Extension;

public class JsonDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => DateTime.Parse(reader.GetString()!);

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString("dd.MM.yyyy HH:mm:ss"));
}

public class JsonNullableDateTimeConverter : JsonConverter<DateTime?>
{
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();
        return string.IsNullOrWhiteSpace(str) ? null : DateTime.Parse(str);
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        // Проверяем на default значение DateTime
        if (value.HasValue && value.Value != default(DateTime) && value.Value != DateTime.MinValue)
            writer.WriteStringValue(value.Value.ToString("dd.MM.yyyy HH:mm:ss"));
        else
            writer.WriteNullValue(); // Это вернет null в JSON
    }
}