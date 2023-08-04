namespace TempStick;
public partial class Message
{
    [JsonPropertyName("temperature"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double Temperature { get; set; }

    [JsonPropertyName("humidity"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double Humidity { get; set; }

    [JsonPropertyName("voltage"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Voltage { get; set; }

    [JsonPropertyName("RSSI"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Rssi { get; set; }

    [JsonPropertyName("time_to_connect"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string TimeToConnect { get; set; }

    [JsonPropertyName("sensor_time_utc"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string SensorTimeUtc { get; set; }

    private IDictionary<string, object> _additionalProperties;

    [JsonExtensionData]
    public IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties ?? (_additionalProperties = new Dictionary<string, object>()); }
        set { _additionalProperties = value; }
    }

}
