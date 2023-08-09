namespace TempStick;

public partial class SensorData : BaseAdditionalData
{
    [JsonPropertyName("version")]
    public string? Version { get; set; }

    [JsonPropertyName("sensor_id")]
    public string? SensorId { get; set; }

    [JsonPropertyName("sensor_name")]
    public string? SensorName { get; set; }

    [JsonPropertyName("sensor_mac_addr")]
    public string? SensorMacAddress { get; set; }

    [JsonPropertyName("owner_id")]
    public string? OwnerId { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("alert_interval")]
    public string? AlertInterval { get; set; }

    [JsonPropertyName("send_interval")]
    public string? SendInterval { get; set; }

    [JsonPropertyName("last_temp")]
    public double LastTemperature { get; set; }

    [JsonPropertyName("last_humidity")]
    public double LastHumidity { get; set; }

    [JsonPropertyName("last_voltage")]
    public double LastVoltage { get; set; }

    [JsonPropertyName("battery_pct")]
    public double BatteryPercentage { get; set; }

    [JsonPropertyName("wifi_connect_time")]
    public int WifiConnectTime { get; set; }

    [JsonPropertyName("rssi")]
    public int Rssi { get; set; }

    [JsonPropertyName("last_checkin")]
    public string? LastCheckin { get; set; }

    [JsonPropertyName("next_checkin")]
    public string? NextCheckin { get; set; }

    [JsonPropertyName("ssid")]
    public string? Ssid { get; set; }

    [JsonPropertyName("offline")]
    public string? Offline { get; set; }

    [JsonPropertyName("alerts")]
    public ICollection<string>? Alerts { get; set; }

    [JsonPropertyName("use_sensor_settings")]
    public int UseSensorSettings { get; set; }

    [JsonPropertyName("temp_offset")]
    public double TempOffset { get; set; }

    [JsonPropertyName("humidity_offset")]
    public double HumidityOffset { get; set; }

    [JsonPropertyName("alert_temp_below")]
    public string? AlertTempBelow { get; set; }

    [JsonPropertyName("alert_temp_above")]
    public string? AlertTempAbove { get; set; }

    [JsonPropertyName("alert_humidity_below")]
    public string? AlertHumidityBelow { get; set; }

    [JsonPropertyName("alert_humidity_above")]
    public string? AlertHumidityAbove { get; set; }

    [JsonPropertyName("connection_sensitivity")]
    public double ConnectionSensitivity { get; set; }

    [JsonPropertyName("use_alert_interval")]
    public int UseAlertInterval { get; set; }

    [JsonPropertyName("use_offset")]
    public int UseOffset { get; set; }

    [JsonPropertyName("last_messages")]
    public ICollection<Message>? LastMessages { get; set; }

}
