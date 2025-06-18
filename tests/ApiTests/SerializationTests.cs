using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TempStick;

namespace ApiTests;

[TestClass]
public class SerializationTests
{
    [TestMethod]
    public void UserResponse_FullSerialization_WorksCorrectly()
    {
        // Arrange
        var user = new User
        {
            Id = "user123",
            Email = "test@example.com",
            FirstName = "John",
            LastName = "Doe",
            WeeklyReportDay = DayOfWeek.Wednesday,
            UseLocalTimeZone = true,
            ChartFill = false
        };
        var userResponse = new UserResponse
        {
            User = user,
            Type = "success",
            Message = "User retrieved successfully"
        };

        // Act
        var json = JsonSerializer.Serialize(userResponse);
        var deserialized = JsonSerializer.Deserialize<UserResponse>(json);

        // Assert
        Assert.IsNotNull(deserialized);
        Assert.AreEqual("success", deserialized.Type);
        Assert.AreEqual("User retrieved successfully", deserialized.Message);
        Assert.AreEqual("user123", deserialized.User.Id);
        Assert.AreEqual("test@example.com", deserialized.User.Email);
        Assert.AreEqual("John", deserialized.User.FirstName);
        Assert.AreEqual("Doe", deserialized.User.LastName);
        Assert.AreEqual(DayOfWeek.Wednesday, deserialized.User.WeeklyReportDay);
        Assert.IsTrue(deserialized.User.UseLocalTimeZone);
        Assert.IsFalse(deserialized.User.ChartFill);
    }

    [TestMethod]
    public void SensorResponse_FullSerialization_WorksCorrectly()
    {
        // Arrange
        var sensorData = new SensorData
        {
            SensorId = "SENSOR123",
            SensorName = "Living Room",
            LastTemperature = 22.5,
            LastHumidity = 45.0,
            BatteryPercentage = 85.0,
            Offline = false,
            UseSensorSettings = true
        };
        var sensorResponse = new SensorResponse
        {
            Data = sensorData,
            Type = "success",
            Message = "Sensor retrieved successfully"
        };

        // Act
        var json = JsonSerializer.Serialize(sensorResponse);
        var deserialized = JsonSerializer.Deserialize<SensorResponse>(json);

        // Assert
        Assert.IsNotNull(deserialized);
        Assert.AreEqual("success", deserialized.Type);
        Assert.AreEqual("Sensor retrieved successfully", deserialized.Message);
        Assert.AreEqual("SENSOR123", deserialized.Data.SensorId);
        Assert.AreEqual("Living Room", deserialized.Data.SensorName);
        Assert.AreEqual(22.5, deserialized.Data.LastTemperature);
        Assert.AreEqual(45.0, deserialized.Data.LastHumidity);
        Assert.AreEqual(85.0, deserialized.Data.BatteryPercentage);
        Assert.IsFalse(deserialized.Data.Offline);
        Assert.IsTrue(deserialized.Data.UseSensorSettings);
    }

    [TestMethod]
    public void ReadingResponse_FullSerialization_WorksCorrectly()
    {
        // Arrange
        var reading1 = new Reading
        {
            Temperature = 20.5,
            Humidity = 40.0,
            SensorTime = "2025-06-18T10:00:00Z",
            Offline = false
        };
        var reading2 = new Reading
        {
            Temperature = 21.0,
            Humidity = 41.0,
            SensorTime = "2025-06-18T11:00:00Z",
            Offline = false
        };
        var readingData = new ReadingData
        {
            Readings = new List<Reading> { reading1, reading2 }
        };
        var readingResponse = new ReadingResponse
        {
            Data = readingData,
            Type = "success",
            Message = "Readings retrieved successfully"
        };

        // Act
        var json = JsonSerializer.Serialize(readingResponse);
        var deserialized = JsonSerializer.Deserialize<ReadingResponse>(json);

        // Assert
        Assert.IsNotNull(deserialized);
        Assert.AreEqual("success", deserialized.Type);
        Assert.AreEqual("Readings retrieved successfully", deserialized.Message);
        Assert.AreEqual(2, deserialized.Data.Readings!.Count);
        Assert.AreEqual(20.5, deserialized.Data.Readings.First().Temperature);
        Assert.AreEqual(40.0, deserialized.Data.Readings.First().Humidity);
        Assert.AreEqual("2025-06-18T10:00:00Z", deserialized.Data.Readings.First().SensorTime);
        Assert.IsFalse(deserialized.Data.Readings.First().Offline);
    }

    [TestMethod]
    public void SensorsDataItem_AllProperties_CanBeSetAndGet()
    {
        // Arrange
        var item = new SensorsDataItem();

        // Act
        item.Version = "1.0.0";
        item.SensorId = "TEMP001";
        item.SensorName = "Kitchen Sensor";
        item.SensorMacAddress = "AA:BB:CC:DD:EE:FF";
        item.OwnerId = "owner123";
        item.Type = "TempStick";
        item.AlertInterval = "15";
        item.SendInterval = "300";
        item.LastTemperature = 23.5;
        item.LastHumidity = 48.0;
        item.LastVoltage = "3.7V";
        item.BatteryPercentage = 78.0;
        item.WifiConnectTime = "1200";
        item.Rssi = "-45";
        item.LastCheckin = "2025-06-18T12:00:00Z";
        item.NextCheckin = "2025-06-18T12:05:00Z";
        item.Ssid = "MyNetwork";
        item.Offline = "0";
        item.TempOffset = 1.0;
        item.HumidityOffset = -2.0;
        item.Group = 1.0;

        // Assert
        Assert.AreEqual("1.0.0", item.Version);
        Assert.AreEqual("TEMP001", item.SensorId);
        Assert.AreEqual("Kitchen Sensor", item.SensorName);
        Assert.AreEqual("AA:BB:CC:DD:EE:FF", item.SensorMacAddress);
        Assert.AreEqual("owner123", item.OwnerId);
        Assert.AreEqual("TempStick", item.Type);
        Assert.AreEqual("15", item.AlertInterval);
        Assert.AreEqual("300", item.SendInterval);
        Assert.AreEqual(23.5, item.LastTemperature);
        Assert.AreEqual(48.0, item.LastHumidity);
        Assert.AreEqual("3.7V", item.LastVoltage);
        Assert.AreEqual(78.0, item.BatteryPercentage);
        Assert.AreEqual("1200", item.WifiConnectTime);
        Assert.AreEqual("-45", item.Rssi);
        Assert.AreEqual("2025-06-18T12:00:00Z", item.LastCheckin);
        Assert.AreEqual("2025-06-18T12:05:00Z", item.NextCheckin);
        Assert.AreEqual("MyNetwork", item.Ssid);
        Assert.AreEqual("0", item.Offline);
        Assert.AreEqual(1.0, item.TempOffset);
        Assert.AreEqual(-2.0, item.HumidityOffset);
        Assert.AreEqual(1.0, item.Group);
    }

    [TestMethod]
    public void SensorsDataItem_JsonDeserialization_WorksCorrectly()
    {
        // Arrange
        var json = """
        {
            "version": "2.1.0",
            "sensor_id": "TEMP002",
            "sensor_name": "Garage Sensor",
            "sensor_mac_addr": "11:22:33:44:55:66",
            "owner_id": "owner456",
            "type": "TempStick Pro",
            "alert_interval": "30",
            "send_interval": "600",
            "last_temp": 5.5,
            "last_humidity": 85.0,
            "last_voltage": "3.5V",
            "battery_pct": 45.0,
            "wifi_connect_time": "2000",
            "rssi": "-65",
            "last_checkin": "2025-06-18T08:00:00Z",
            "next_checkin": "2025-06-18T08:10:00Z",
            "ssid": "GarageWiFi",
            "offline": "1",
            "temp_offset": 0.5,
            "humidity_offset": 1.5,
            "group": 2.0
        }
        """;

        // Act
        var item = JsonSerializer.Deserialize<SensorsDataItem>(json);

        // Assert
        Assert.IsNotNull(item);
        Assert.AreEqual("2.1.0", item.Version);
        Assert.AreEqual("TEMP002", item.SensorId);
        Assert.AreEqual("Garage Sensor", item.SensorName);
        Assert.AreEqual("11:22:33:44:55:66", item.SensorMacAddress);
        Assert.AreEqual("owner456", item.OwnerId);
        Assert.AreEqual("TempStick Pro", item.Type);
        Assert.AreEqual("30", item.AlertInterval);
        Assert.AreEqual("600", item.SendInterval);
        Assert.AreEqual(5.5, item.LastTemperature);
        Assert.AreEqual(85.0, item.LastHumidity);
        Assert.AreEqual("3.5V", item.LastVoltage);
        Assert.AreEqual(45.0, item.BatteryPercentage);
        Assert.AreEqual("2000", item.WifiConnectTime);
        Assert.AreEqual("-65", item.Rssi);
        Assert.AreEqual("2025-06-18T08:00:00Z", item.LastCheckin);
        Assert.AreEqual("2025-06-18T08:10:00Z", item.NextCheckin);
        Assert.AreEqual("GarageWiFi", item.Ssid);
        Assert.AreEqual("1", item.Offline);
        Assert.AreEqual(0.5, item.TempOffset);
        Assert.AreEqual(1.5, item.HumidityOffset);
        Assert.AreEqual(2.0, item.Group);
    }

    [TestMethod]
    public void Extensions_TemperatureConversion_EdgeCases()
    {
        // Test absolute zero
        Assert.AreEqual(-459.67, (-273.15).AsFahrenheit(), 0.01);

        // Test a very high temperature  
        Assert.AreEqual(1832.0, 1000.0.AsFahrenheit(), 0.01);

        // Test fractional values
        Assert.AreEqual(98.6, 37.0.AsFahrenheit(), 0.01);
    }

    [TestMethod]
    public void Extensions_SignalQuality_EdgeCases()
    {
        // Test signal at exactly -100 dbm (should be 0%)
        Assert.AreEqual("0%", (-100.0).AsSignalQuality());

        // Test very strong signal (should be high percentage)
        Assert.AreEqual("200%", 0.0.AsSignalQuality());

        // Test negative signal beyond -100
        Assert.AreEqual("-20%", (-110.0).AsSignalQuality());
    }

    [TestMethod]
    public void User_DefaultPropertyValues_AreCorrect()
    {
        // Arrange & Act
        var user = new User { Id = "test" };

        // Assert - Test that numeric properties default to 0
        Assert.AreEqual(0, user.Level);
        Assert.AreEqual(0, user.SensorCount);
        Assert.AreEqual(DayOfWeek.Sunday, user.WeeklyReportDay); // Default enum value

        // Boolean properties should default to false
        Assert.IsFalse(user.UseLocalTimeZone);
        Assert.IsFalse(user.UseSensorGroups);
        Assert.IsFalse(user.ChartFill);
        Assert.IsFalse(user.SendReports);
        Assert.IsFalse(user.DailyReports);
        Assert.IsFalse(user.WeeklyReports);
        Assert.IsFalse(user.MonthlyReports);
        Assert.IsFalse(user.ReportsSpecificSensors);
        Assert.IsFalse(user.IsSubUser);
    }

    [TestMethod]
    public void SensorData_DefaultPropertyValues_AreCorrect()
    {
        // Arrange & Act
        var sensorData = new SensorData();

        // Assert - Test that numeric properties default to 0
        Assert.AreEqual(0.0, sensorData.LastTemperature);
        Assert.AreEqual(0.0, sensorData.LastHumidity);
        Assert.AreEqual(0.0, sensorData.LastVoltage);
        Assert.AreEqual(0.0, sensorData.BatteryPercentage);
        Assert.AreEqual(0, sensorData.WifiConnectTime);
        Assert.AreEqual(0, sensorData.Rssi);
        Assert.AreEqual(0.0, sensorData.TempOffset);
        Assert.AreEqual(0.0, sensorData.HumidityOffset);
        Assert.AreEqual(0.0, sensorData.ConnectionSensitivity);

        // Boolean properties should default to false
        Assert.IsFalse(sensorData.Offline);
        Assert.IsFalse(sensorData.UseSensorSettings);
        Assert.IsFalse(sensorData.UseAlertInterval);
        Assert.IsFalse(sensorData.UseOffset);
    }
}
