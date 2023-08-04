namespace TempStick;

public partial class SensorResponse
{
    [JsonPropertyName("type"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Type { get; set; }

    [JsonPropertyName("message"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Message { get; set; }

    [JsonPropertyName("data"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SensorData Data { get; set; }

    private IDictionary<string, object> _additionalProperties;

    [JsonExtensionData]
    public IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties ?? (_additionalProperties = new Dictionary<string, object>()); }
        set { _additionalProperties = value; }
    }

}
