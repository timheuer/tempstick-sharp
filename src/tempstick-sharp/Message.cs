namespace TempStick;
public partial class Message
{
    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    [JsonPropertyName("humidity")]
    public double Humidity { get; set; }

    [JsonPropertyName("voltage")]
    public string Voltage { get; set; }

    [JsonPropertyName("RSSI")]
    public string Rssi { get; set; }

    [JsonPropertyName("time_to_connect")]
    public string TimeToConnect { get; set; }

    [JsonPropertyName("sensor_time_utc")]
    public string SensorTimeUtc { get; set; }

    private IDictionary<string, object> _additionalProperties;

    [JsonExtensionData]
    public IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties ?? (_additionalProperties = new Dictionary<string, object>()); }
        set { _additionalProperties = value; }
    }

}
