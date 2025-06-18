using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TempStick;

namespace ApiTests;

[TestClass]
public class ResponseModelTests
{
    [TestMethod]
    public void UserResponse_Properties_CanBeSetAndGet()
    {
        // Arrange
        var user = new User { Id = "user123", Email = "test@example.com" };
        var userResponse = new UserResponse { User = user };

        // Act
        userResponse.Type = "success";
        userResponse.Message = "User retrieved successfully";

        // Assert
        Assert.AreEqual("success", userResponse.Type);
        Assert.AreEqual("User retrieved successfully", userResponse.Message);
        Assert.AreEqual(user, userResponse.User);
        Assert.AreEqual("user123", userResponse.User.Id);
    }

    [TestMethod]
    public void UserResponse_InheritsFromBaseResponseMetadata()
    {
        // Arrange
        var user = new User { Id = "user123" };
        var userResponse = new UserResponse { User = user };

        // Act
        userResponse.AdditionalProperties["customField"] = "customValue";

        // Assert
        Assert.IsInstanceOfType(userResponse, typeof(BaseResponseMetadata));
        Assert.AreEqual("customValue", userResponse.AdditionalProperties["customField"]);
    }

    [TestMethod]
    public void SensorResponse_Properties_CanBeSetAndGet()
    {
        // Arrange
        var sensorResponse = new SensorResponse();
        var sensorData = new SensorData { SensorId = "SENSOR123", SensorName = "Test Sensor" };

        // Act
        sensorResponse.Type = "success";
        sensorResponse.Message = "Sensor retrieved successfully";
        sensorResponse.Data = sensorData;

        // Assert
        Assert.AreEqual("success", sensorResponse.Type);
        Assert.AreEqual("Sensor retrieved successfully", sensorResponse.Message);
        Assert.AreEqual(sensorData, sensorResponse.Data);
        Assert.AreEqual("SENSOR123", sensorResponse.Data.SensorId);
    }
    [TestMethod]
    public void SensorsResponse_Properties_CanBeSetAndGet()
    {
        // Arrange
        var sensorsResponse = new SensorsResponse();
        var sensorsData = new SensorsData();
        var sensorList = new List<SensorsDataItem>
        {
            new SensorsDataItem { SensorId = "SENSOR1", SensorName = "Sensor 1" },
            new SensorsDataItem { SensorId = "SENSOR2", SensorName = "Sensor 2" }
        };
        sensorsData.Sensors = sensorList;

        // Act
        sensorsResponse.Type = "success";
        sensorsResponse.Message = "Sensors retrieved successfully";
        sensorsResponse.Data = sensorsData;

        // Assert
        Assert.AreEqual("success", sensorsResponse.Type);
        Assert.AreEqual("Sensors retrieved successfully", sensorsResponse.Message);
        Assert.AreEqual(sensorsData, sensorsResponse.Data);
        Assert.AreEqual(2, sensorsResponse.Data.Sensors!.Count);
    }

    [TestMethod]
    public void ReadingResponse_Properties_CanBeSetAndGet()
    {
        // Arrange
        var readingResponse = new ReadingResponse();
        var readingData = new ReadingData();
        var readings = new List<Reading>
        {
            new Reading { Temperature = 22.5, Humidity = 45.0, SensorTime = "2025-06-18T10:00:00Z" },
            new Reading { Temperature = 23.0, Humidity = 46.0, SensorTime = "2025-06-18T11:00:00Z" }
        };
        readingData.Readings = readings;

        // Act
        readingResponse.Type = "success";
        readingResponse.Message = "Readings retrieved successfully";
        readingResponse.Data = readingData;

        // Assert
        Assert.AreEqual("success", readingResponse.Type);
        Assert.AreEqual("Readings retrieved successfully", readingResponse.Message);
        Assert.AreEqual(readingData, readingResponse.Data);
        Assert.AreEqual(2, readingResponse.Data.Readings.Count);
    }
    [TestMethod]
    public void SensorsData_Properties_CanBeSetAndGet()
    {
        // Arrange
        var sensorsData = new SensorsData();
        var sensors = new List<SensorsDataItem>
        {
            new SensorsDataItem { SensorId = "TEMP001", SensorName = "Living Room" },
            new SensorsDataItem { SensorId = "TEMP002", SensorName = "Bedroom" }
        };

        // Act
        sensorsData.Sensors = sensors;

        // Assert
        Assert.AreEqual(2, sensorsData.Sensors.Count);
        Assert.AreEqual("TEMP001", sensorsData.Sensors.First().SensorId);
        Assert.AreEqual("Living Room", sensorsData.Sensors.First().SensorName);
    }

    [TestMethod]
    public void SensorsData_WithNullSensors_HandlesCorrectly()
    {
        // Arrange
        var sensorsData = new SensorsData();

        // Act
        sensorsData.Sensors = null;

        // Assert
        Assert.IsNull(sensorsData.Sensors);
    }

    [TestMethod]
    public void ReadingData_Properties_CanBeSetAndGet()
    {
        // Arrange
        var readingData = new ReadingData();
        var readings = new List<Reading>
        {
            new Reading { Temperature = 18.5, Humidity = 40.0, Offline = false },
            new Reading { Temperature = 19.0, Humidity = 41.0, Offline = false }
        };

        // Act
        readingData.Readings = readings;

        // Assert
        Assert.AreEqual(readings, readingData.Readings);
        Assert.AreEqual(2, readingData.Readings.Count);
        Assert.AreEqual(18.5, readingData.Readings.First().Temperature);
        Assert.AreEqual(40.0, readingData.Readings.First().Humidity);
    }

    [TestMethod]
    public void ReadingData_InheritsFromBaseAdditionalData()
    {
        // Arrange
        var readingData = new ReadingData();

        // Act
        readingData.AdditionalProperties["sensorId"] = "SENSOR123";
        readingData.AdditionalProperties["interval"] = "5min";

        // Assert
        Assert.IsInstanceOfType(readingData, typeof(BaseAdditionalData));
        Assert.AreEqual("SENSOR123", readingData.AdditionalProperties["sensorId"]);
        Assert.AreEqual("5min", readingData.AdditionalProperties["interval"]);
    }

    [TestMethod]
    public void Message_Properties_CanBeSetAndGet()
    {
        // Arrange
        var message = new Message();

        // Act
        message.Temperature = 24.5;
        message.Humidity = 52.0;
        message.Voltage = "3.8V";
        message.Rssi = "-45";
        message.TimeToConnect = "800";
        message.SensorTimeUtc = "2025-06-18T12:30:00Z";

        // Assert
        Assert.AreEqual(24.5, message.Temperature);
        Assert.AreEqual(52.0, message.Humidity);
        Assert.AreEqual("3.8V", message.Voltage);
        Assert.AreEqual("-45", message.Rssi);
        Assert.AreEqual("800", message.TimeToConnect);
        Assert.AreEqual("2025-06-18T12:30:00Z", message.SensorTimeUtc);
    }

    [TestMethod]
    public void Message_InheritsFromBaseAdditionalData()
    {
        // Arrange
        var message = new Message();

        // Act
        message.AdditionalProperties["location"] = "Kitchen";
        message.AdditionalProperties["alertLevel"] = "normal";

        // Assert
        Assert.IsInstanceOfType(message, typeof(BaseAdditionalData));
        Assert.AreEqual("Kitchen", message.AdditionalProperties["location"]);
        Assert.AreEqual("normal", message.AdditionalProperties["alertLevel"]);
    }

    [TestMethod]
    public void Message_JsonDeserialization_WorksCorrectly()
    {
        // Arrange
        var json = """
        {
            "temperature": 21.5,
            "humidity": 48.0,
            "voltage": "3.7V",
            "RSSI": "-52",
            "time_to_connect": "1000",
            "sensor_time_utc": "2025-06-18T09:15:00Z"
        }
        """;

        // Act
        var message = JsonSerializer.Deserialize<Message>(json);

        // Assert
        Assert.IsNotNull(message);
        Assert.AreEqual(21.5, message.Temperature);
        Assert.AreEqual(48.0, message.Humidity);
        Assert.AreEqual("3.7V", message.Voltage);
        Assert.AreEqual("-52", message.Rssi);
        Assert.AreEqual("1000", message.TimeToConnect);
        Assert.AreEqual("2025-06-18T09:15:00Z", message.SensorTimeUtc);
    }
}
