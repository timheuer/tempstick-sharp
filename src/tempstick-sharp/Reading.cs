namespace TempStick;

public partial class Reading : BaseAdditionalData
{
    [JsonPropertyName("sensor_time")]
    public string? SensorTime { get; set; }

    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    [JsonPropertyName("humidity")]
    public double Humidity { get; set; }

    [JsonPropertyName("offline")]
    [JsonConverter(typeof(BooleanConverter))]
    public bool Offline { get; set; }

}