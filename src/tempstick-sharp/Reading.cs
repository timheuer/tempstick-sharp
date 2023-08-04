namespace TempStick;

public partial class Reading
{
    [JsonPropertyName("sensor_time"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string SensorTime { get; set; }

    [JsonPropertyName("temperature"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double Temperature { get; set; }

    [JsonPropertyName("humidity"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double Humidity { get; set; }

    [JsonPropertyName("offline"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Offline { get; set; }

    private IDictionary<string, object> _additionalProperties;

    [JsonExtensionData]
    public IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties ?? (_additionalProperties = new Dictionary<string, object>()); }
        set { _additionalProperties = value; }
    }

}