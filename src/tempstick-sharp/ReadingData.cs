namespace TempStick;

public partial class ReadingData
{
    [JsonPropertyName("start")]
    public string Start { get; set; }

    [JsonPropertyName("end")]
    public string End { get; set; }

    [JsonPropertyName("readings")]
    public ICollection<Reading> Readings { get; set; }

    private IDictionary<string, object> _additionalProperties;

    [JsonExtensionData]
    public IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties ?? (_additionalProperties = new Dictionary<string, object>()); }
        set { _additionalProperties = value; }
    }

}