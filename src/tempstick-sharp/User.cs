using TempStickSharp.Helpers;

namespace TempStickSharp;
public class User
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
    
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("contact_email")]
    public string? ContactEmail { get; set; }

    [JsonPropertyName("address_1")]
    public string? Address { get; set; }

    [JsonPropertyName("address_2")]
    public string? Address2 { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("zip")]
    public string? PostalCode { get; set; }

    [JsonPropertyName("temp_pref")]
    public string? TemperatureScale { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    [JsonPropertyName("use_local_timezone")]
    [JsonConverter(typeof(BooleanConverter))]
    public bool UseLocalTimeZone { get; set; }

    [JsonPropertyName("use_sensor_groups")]
    [JsonConverter(typeof(BooleanConverter))]
    public bool UseSensorGroups { get; set; }

    [JsonPropertyName("chart_fill")]
    [JsonConverter(typeof(BooleanConverter))]
    public bool ChartFill {  get; set; }

    [JsonPropertyName("send_reports")]
    [JsonConverter(typeof(BooleanConverter))]
    public bool SendReports { get; set; }

    [JsonPropertyName("daily_reports")]
    [JsonConverter(typeof(BooleanConverter))]
    public bool DailyReports { get; set; }

    [JsonPropertyName("weekly_reports")]
    [JsonConverter(typeof(BooleanConverter))]
    public bool WeeklyReports { get; set; }

    [JsonPropertyName("monthly_reports")]
    [JsonConverter(typeof(BooleanConverter))]
    public bool MonthlyReports { get; set; }

    [JsonPropertyName("reports_specific_sensors")]
    [JsonConverter(typeof(BooleanConverter))]
    public bool ReportsSpecificSensors { get; set; }

    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("weekly_report_day")]
    [JsonConverter(typeof(DayOfWeekConverter))]
    public DayOfWeek WeeklyReportDay { get; set; }

    [JsonPropertyName("sensor_count")]
    public int SensorCount { get; set; }

    [JsonPropertyName("is_sub_user")]
    [JsonConverter(typeof(BooleanConverter))]
    public bool IsSubUser { get; set; }
}
