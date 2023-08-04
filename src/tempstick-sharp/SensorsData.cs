namespace TempStick;

public partial class SensorsData
{

    [JsonPropertyName("items"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<SensorsDataItem> Sensors { get; set; }
}

public partial class SensorsDataItem
{
    [JsonPropertyName("version"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Version { get; set; }

    [JsonPropertyName("sensor_id"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string SensorId { get; set; }

    [JsonPropertyName("sensor_name"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string SensorName { get; set; }

    [JsonPropertyName("sensor_mac_addr"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string SensorMacAddress { get; set; }

    [JsonPropertyName("owner_id"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string OwnerId { get; set; }

    [JsonPropertyName("type"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Type { get; set; }

    [JsonPropertyName("alert_interval"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string AlertInterval { get; set; }

    [JsonPropertyName("send_interval"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string SendInterval { get; set; }

    [JsonPropertyName("last_temp"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double LastTemperature { get; set; }

    [JsonPropertyName("last_humidity"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double LastHumidity { get; set; }

    [JsonPropertyName("last_voltage"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string LastVoltage { get; set; }

    [JsonPropertyName("battery_pct"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double BatteryPercentage { get; set; }

    [JsonPropertyName("wifi_connect_time"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string WifiConnectTime { get; set; }

    [JsonPropertyName("rssi"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Rssi { get; set; }

    [JsonPropertyName("last_checkin"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string LastCheckin { get; set; }

    [JsonPropertyName("next_checkin"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string NextCheckin { get; set; }

    [JsonPropertyName("ssid"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Ssid { get; set; }

    [JsonPropertyName("offline"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Offline { get; set; }

    [JsonPropertyName("temp_offset"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double TempOffset { get; set; }

    [JsonPropertyName("humidity_offset"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double HumidityOffset { get; set; }

    [JsonPropertyName("group"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double Group { get; set; }
}