namespace TempStick;

public partial class SensorResponse : BaseResponseMetadata
{
    [JsonPropertyName("data")]
    public SensorData? Data { get; set; }

}
