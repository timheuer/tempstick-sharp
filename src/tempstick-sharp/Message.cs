namespace TempStick;
public partial class Message : BaseAdditionalData
{
    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    [JsonPropertyName("humidity")]
    public double Humidity { get; set; }

    [JsonPropertyName("voltage")]
    public string? Voltage { get; set; }

    [JsonPropertyName("RSSI")]
    public string? Rssi { get; set; }

    [JsonPropertyName("time_to_connect")]
    public string? TimeToConnect { get; set; }

    [JsonPropertyName("sensor_time_utc")]
    public string? SensorTimeUtc { get; set; }

}
