namespace TempStick;
public partial class ReadingResponse : BaseResponseMetadata
{
    [JsonPropertyName("data")]
    public ReadingData? Data { get; set; }

}
