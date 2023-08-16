namespace TempStick;

public class BooleanConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            return string.Equals(reader.GetString(), "1", StringComparison.OrdinalIgnoreCase);
        }
        return reader.TryGetInt32(out var value) && value == 1;

    }

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
        // NOTE: This assumes that a string 1/0 will work. TempStick API sometimes has defaults as int but then when changed they suddenly are strings
        writer.WriteStringValue(value ? "1" : "0");
    }
}

public class DayOfWeekConverter : JsonConverter<DayOfWeek>
{
    public override DayOfWeek Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueAsString = reader.GetString();
        DayOfWeek day = (DayOfWeek)(Convert.ToInt32(valueAsString) - 1);
        return day;
    }

    public override void Write(Utf8JsonWriter writer, DayOfWeek value, JsonSerializerOptions options)
    {
        int newDay = ((int)value) + 1;
        writer.WriteStringValue(newDay.ToString());
    }
}
