﻿namespace TempStick;
public class UserResponse : BaseResponseMetadata
{
    [JsonPropertyName("data")]
    public required User User { get; set; }
}
