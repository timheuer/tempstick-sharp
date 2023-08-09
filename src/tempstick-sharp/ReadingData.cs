namespace TempStick;

public partial class ReadingData : BaseAdditionalData
{
    [JsonPropertyName("start")]
    public string? Start { get; set; }

    [JsonPropertyName("end")]
    public string? End { get; set; }

    [JsonPropertyName("readings")]
    public ICollection<Reading>? Readings { get; set; }

}