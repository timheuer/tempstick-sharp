namespace TempStick;

public partial class ReadingData
{
    [JsonPropertyName("start"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Start { get; set; }

    [JsonPropertyName("end"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string End { get; set; }

    [JsonPropertyName("readings"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<Reading> Readings { get; set; }

    private IDictionary<string, object> _additionalProperties;

    [JsonExtensionData]
    public IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties ?? (_additionalProperties = new Dictionary<string, object>()); }
        set { _additionalProperties = value; }
    }

}