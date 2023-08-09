namespace TempStick;

public partial class SensorsResponse : BaseResponseMetadata
{
    [JsonPropertyName("data")]
    public SensorsData? Data { get; set; }

}