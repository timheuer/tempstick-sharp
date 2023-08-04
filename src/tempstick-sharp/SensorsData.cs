namespace TempStick;

public partial class SensorsData
{

    [JsonPropertyName("items")]
    public ICollection<SensorsDataItem> Sensors { get; set; }
}

public partial class SensorsDataItem
{
    [JsonPropertyName("version")]
    public string Version { get; set; }

    [JsonPropertyName("sensor_id")]
    public string SensorId { get; set; }

    [JsonPropertyName("sensor_name")]
    public string SensorName { get; set; }

    [JsonPropertyName("sensor_mac_addr")]
    public string SensorMacAddress { get; set; }

    [JsonPropertyName("owner_id")]
    public string OwnerId { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("alert_interval")]
    public string AlertInterval { get; set; }

    [JsonPropertyName("send_interval")]
    public string SendInterval { get; set; }

    [JsonPropertyName("last_temp")]
    public double LastTemperature { get; set; }

    [JsonPropertyName("last_humidity")]
    public double LastHumidity { get; set; }

    [JsonPropertyName("last_voltage")]
    public string LastVoltage { get; set; }

    [JsonPropertyName("battery_pct")]
    public double BatteryPercentage { get; set; }

    [JsonPropertyName("wifi_connect_time")]
    public string WifiConnectTime { get; set; }

    [JsonPropertyName("rssi")]
    public string Rssi { get; set; }

    [JsonPropertyName("last_checkin")]
    public string LastCheckin { get; set; }

    [JsonPropertyName("next_checkin")]
    public string NextCheckin { get; set; }

    [JsonPropertyName("ssid")]
    public string Ssid { get; set; }

    [JsonPropertyName("offline")]
    public string Offline { get; set; }

    [JsonPropertyName("temp_offset")]
    public double TempOffset { get; set; }

    [JsonPropertyName("humidity_offset")]
    public double HumidityOffset { get; set; }

    [JsonPropertyName("group")]
    public double Group { get; set; }
}