namespace TempStick;

public partial class BaseResponseMetadata : BaseAdditionalData
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

}