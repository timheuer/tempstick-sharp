using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TempStick;

namespace ApiTests;

[TestClass]
public class ModelTests
{
    [TestMethod]
    public void BaseAdditionalData_AdditionalProperties_InitializesLazily()
    {
        // Arrange
        var baseData = new TestBaseAdditionalData();

        // Act
        var additionalProps = baseData.AdditionalProperties;

        // Assert
        Assert.IsNotNull(additionalProps);
        Assert.AreEqual(0, additionalProps.Count);
    }

    [TestMethod]
    public void BaseAdditionalData_AdditionalProperties_CanSetAndGet()
    {
        // Arrange
        var baseData = new TestBaseAdditionalData();
        var testData = new Dictionary<string, object>
        {
            ["customProperty"] = "customValue",
            ["numericProperty"] = 42
        };

        // Act
        baseData.AdditionalProperties = testData;

        // Assert
        Assert.AreEqual(testData, baseData.AdditionalProperties);
        Assert.AreEqual("customValue", baseData.AdditionalProperties["customProperty"]);
        Assert.AreEqual(42, baseData.AdditionalProperties["numericProperty"]);
    }

    [TestMethod]
    public void BaseAdditionalData_AdditionalProperties_CanAddItems()
    {
        // Arrange
        var baseData = new TestBaseAdditionalData();

        // Act
        baseData.AdditionalProperties["key1"] = "value1";
        baseData.AdditionalProperties["key2"] = 123;

        // Assert
        Assert.AreEqual(2, baseData.AdditionalProperties.Count);
        Assert.AreEqual("value1", baseData.AdditionalProperties["key1"]);
        Assert.AreEqual(123, baseData.AdditionalProperties["key2"]);
    }

    [TestMethod]
    public void BaseResponseMetadata_Properties_CanBeSetAndGet()
    {
        // Arrange
        var metadata = new BaseResponseMetadata();

        // Act
        metadata.Type = "success";
        metadata.Message = "Operation completed successfully";

        // Assert
        Assert.AreEqual("success", metadata.Type);
        Assert.AreEqual("Operation completed successfully", metadata.Message);
    }

    [TestMethod]
    public void BaseResponseMetadata_InheritsFromBaseAdditionalData()
    {
        // Arrange
        var metadata = new BaseResponseMetadata();

        // Act
        metadata.AdditionalProperties["customField"] = "customValue";

        // Assert
        Assert.IsInstanceOfType(metadata, typeof(BaseAdditionalData));
        Assert.AreEqual("customValue", metadata.AdditionalProperties["customField"]);
        Assert.AreEqual(1, metadata.AdditionalProperties.Count);
    }

    [TestMethod]
    public void BaseResponseMetadata_DefaultValues_AreNull()
    {
        // Arrange & Act
        var metadata = new BaseResponseMetadata();

        // Assert
        Assert.IsNull(metadata.Type);
        Assert.IsNull(metadata.Message);
    }

    [TestMethod]
    public void Reading_Properties_CanBeSetAndGet()
    {
        // Arrange
        var reading = new Reading();

        // Act
        reading.SensorTime = "2025-06-18T10:30:00Z";
        reading.Temperature = 23.5;
        reading.Humidity = 45.2;
        reading.Offline = true;

        // Assert
        Assert.AreEqual("2025-06-18T10:30:00Z", reading.SensorTime);
        Assert.AreEqual(23.5, reading.Temperature);
        Assert.AreEqual(45.2, reading.Humidity);
        Assert.IsTrue(reading.Offline);
    }

    [TestMethod]
    public void Reading_InheritsFromBaseAdditionalData()
    {
        // Arrange
        var reading = new Reading();

        // Act
        reading.AdditionalProperties["location"] = "Living Room";

        // Assert
        Assert.IsInstanceOfType(reading, typeof(BaseAdditionalData));
        Assert.AreEqual("Living Room", reading.AdditionalProperties["location"]);
    }

    [TestMethod]
    public void Reading_JsonDeserialization_WorksWithBooleanConverter()
    {
        // Arrange
        var json = """
        {
            "sensor_time": "2025-06-18T10:30:00Z",
            "temperature": 25.0,
            "humidity": 50.0,
            "offline": "1"
        }
        """;

        // Act
        var reading = JsonSerializer.Deserialize<Reading>(json);

        // Assert
        Assert.IsNotNull(reading);
        Assert.AreEqual("2025-06-18T10:30:00Z", reading.SensorTime);
        Assert.AreEqual(25.0, reading.Temperature);
        Assert.AreEqual(50.0, reading.Humidity);
        Assert.IsTrue(reading.Offline);
    }

    [TestMethod]
    public void SensorData_Properties_CanBeSetAndGet()
    {
        // Arrange
        var sensorData = new SensorData(); var alerts = new List<string> { "High Temperature", "Low Battery" };
        var messages = new List<Message>
        {
            new Message { Temperature = 25.5, Humidity = 60.0, Voltage = "3.6V", Rssi = "-50", TimeToConnect = "1200", SensorTimeUtc = "2025-06-18T10:00:00Z" }
        };

        // Act
        sensorData.Version = "1.2.3";
        sensorData.SensorId = "SENSOR123";
        sensorData.SensorName = "Living Room Sensor";
        sensorData.SensorMacAddress = "AA:BB:CC:DD:EE:FF";
        sensorData.OwnerId = "USER456";
        sensorData.Type = "TempStick";
        sensorData.AlertInterval = "15";
        sensorData.SendInterval = "60";
        sensorData.LastTemperature = 22.5;
        sensorData.LastHumidity = 45.0;
        sensorData.LastVoltage = 3.7;
        sensorData.BatteryPercentage = 85.0;
        sensorData.WifiConnectTime = 1200;
        sensorData.Rssi = -45;
        sensorData.LastCheckin = "2025-06-18T10:00:00Z";
        sensorData.NextCheckin = "2025-06-18T11:00:00Z";
        sensorData.Ssid = "MyWiFiNetwork";
        sensorData.Offline = false;
        sensorData.Alerts = alerts;
        sensorData.UseSensorSettings = true;
        sensorData.TempOffset = 1.5;
        sensorData.HumidityOffset = -2.0;
        sensorData.AlertTempBelow = "10";
        sensorData.AlertTempAbove = "30";
        sensorData.AlertHumidityBelow = "20";
        sensorData.AlertHumidityAbove = "80";
        sensorData.ConnectionSensitivity = 0.5;
        sensorData.UseAlertInterval = true;
        sensorData.UseOffset = true;
        sensorData.LastMessages = messages;

        // Assert
        Assert.AreEqual("1.2.3", sensorData.Version);
        Assert.AreEqual("SENSOR123", sensorData.SensorId);
        Assert.AreEqual("Living Room Sensor", sensorData.SensorName);
        Assert.AreEqual("AA:BB:CC:DD:EE:FF", sensorData.SensorMacAddress);
        Assert.AreEqual("USER456", sensorData.OwnerId);
        Assert.AreEqual("TempStick", sensorData.Type);
        Assert.AreEqual("15", sensorData.AlertInterval);
        Assert.AreEqual("60", sensorData.SendInterval);
        Assert.AreEqual(22.5, sensorData.LastTemperature);
        Assert.AreEqual(45.0, sensorData.LastHumidity);
        Assert.AreEqual(3.7, sensorData.LastVoltage);
        Assert.AreEqual(85.0, sensorData.BatteryPercentage);
        Assert.AreEqual(1200, sensorData.WifiConnectTime);
        Assert.AreEqual(-45, sensorData.Rssi);
        Assert.AreEqual("2025-06-18T10:00:00Z", sensorData.LastCheckin);
        Assert.AreEqual("2025-06-18T11:00:00Z", sensorData.NextCheckin);
        Assert.AreEqual("MyWiFiNetwork", sensorData.Ssid);
        Assert.IsFalse(sensorData.Offline);
        Assert.AreEqual(alerts, sensorData.Alerts);
        Assert.IsTrue(sensorData.UseSensorSettings);
        Assert.AreEqual(1.5, sensorData.TempOffset);
        Assert.AreEqual(-2.0, sensorData.HumidityOffset);
        Assert.AreEqual("10", sensorData.AlertTempBelow);
        Assert.AreEqual("30", sensorData.AlertTempAbove);
        Assert.AreEqual("20", sensorData.AlertHumidityBelow);
        Assert.AreEqual("80", sensorData.AlertHumidityAbove);
        Assert.AreEqual(0.5, sensorData.ConnectionSensitivity);
        Assert.IsTrue(sensorData.UseAlertInterval);
        Assert.IsTrue(sensorData.UseOffset);
        Assert.AreEqual(messages, sensorData.LastMessages);
    }

    [TestMethod]
    public void User_AllBooleanProperties_WorkWithConverter()
    {
        // Arrange
        var json = """
        {
            "id": "user123",
            "use_local_timezone": "1",
            "use_sensor_groups": 0,
            "chart_fill": "1",
            "send_reports": 1,
            "daily_reports": "0",
            "weekly_reports": 1,
            "monthly_reports": "1",
            "reports_specific_sensors": 0,
            "is_sub_user": "0",
            "weekly_report_day": "3"
        }
        """;

        // Act
        var user = JsonSerializer.Deserialize<User>(json);

        // Assert
        Assert.IsNotNull(user);
        Assert.AreEqual("user123", user.Id);
        Assert.IsTrue(user.UseLocalTimeZone);
        Assert.IsFalse(user.UseSensorGroups);
        Assert.IsTrue(user.ChartFill);
        Assert.IsTrue(user.SendReports);
        Assert.IsFalse(user.DailyReports);
        Assert.IsTrue(user.WeeklyReports);
        Assert.IsTrue(user.MonthlyReports);
        Assert.IsFalse(user.ReportsSpecificSensors);
        Assert.IsFalse(user.IsSubUser);
        Assert.AreEqual(DayOfWeek.Tuesday, user.WeeklyReportDay);
    }

    [TestMethod]
    public void User_AllStringProperties_CanBeSetAndGet()
    {
        // Arrange
        var user = new User { Id = "test" };

        // Act
        user.Email = "test@example.com";
        user.FirstName = "John";
        user.LastName = "Doe";
        user.Phone = "+1234567890";
        user.ContactEmail = "contact@example.com";
        user.Address = "123 Main St";
        user.Address2 = "Apt 4B";
        user.City = "Anytown";
        user.State = "CA";
        user.PostalCode = "12345";
        user.TemperatureScale = "F";
        user.Timezone = "America/Los_Angeles";

        // Assert
        Assert.AreEqual("test@example.com", user.Email);
        Assert.AreEqual("John", user.FirstName);
        Assert.AreEqual("Doe", user.LastName);
        Assert.AreEqual("+1234567890", user.Phone);
        Assert.AreEqual("contact@example.com", user.ContactEmail);
        Assert.AreEqual("123 Main St", user.Address);
        Assert.AreEqual("Apt 4B", user.Address2);
        Assert.AreEqual("Anytown", user.City);
        Assert.AreEqual("CA", user.State);
        Assert.AreEqual("12345", user.PostalCode);
        Assert.AreEqual("F", user.TemperatureScale);
        Assert.AreEqual("America/Los_Angeles", user.Timezone);
    }

    [TestMethod]
    public void User_NumericProperties_CanBeSetAndGet()
    {
        // Arrange
        var user = new User { Id = "test" };

        // Act
        user.Level = 5;
        user.SensorCount = 10;

        // Assert
        Assert.AreEqual(5, user.Level);
        Assert.AreEqual(10, user.SensorCount);
    }

    private class TestBaseAdditionalData : BaseAdditionalData
    {
        // Empty test class to test BaseAdditionalData functionality
    }
}
