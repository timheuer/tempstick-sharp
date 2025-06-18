using System.Reflection;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TempStick;

namespace ApiTests;

[TestClass]
public class ObjectResponseResultTests
{
    [TestMethod]
    public void ObjectResponseResult_Constructor_SetsProperties()
    {
        // Arrange
        var responseObject = "test response";
        var responseText = "response text";

        // Act
        var result = CreateObjectResponseResult(responseObject, responseText);

        // Assert
        Assert.AreEqual(responseObject, GetObjectProperty(result));
        Assert.AreEqual(responseText, GetTextProperty(result));
    }

    [TestMethod]
    public void ObjectResponseResult_Constructor_WithNullObject_HandlesNull()
    {
        // Arrange
        string? responseObject = null;
        var responseText = "response text";

        // Act
        var result = CreateObjectResponseResult(responseObject!, responseText);

        // Assert
        Assert.IsNull(GetObjectProperty(result));
        Assert.AreEqual(responseText, GetTextProperty(result));
    }

    [TestMethod]
    public void ObjectResponseResult_Constructor_WithNullText_HandlesNull()
    {
        // Arrange
        var responseObject = "test response";
        string? responseText = null;

        // Act
        var result = CreateObjectResponseResult(responseObject, responseText!);

        // Assert
        Assert.AreEqual(responseObject, GetObjectProperty(result));
        Assert.IsNull(GetTextProperty(result));
    }

    [TestMethod]
    public void ObjectResponseResult_Constructor_WithBothNull_HandlesBothNull()
    {
        // Arrange
        string? responseObject = null;
        string? responseText = null;

        // Act
        var result = CreateObjectResponseResult(responseObject!, responseText!);

        // Assert
        Assert.IsNull(GetObjectProperty(result));
        Assert.IsNull(GetTextProperty(result));
    }

    [TestMethod]
    public void ObjectResponseResult_Constructor_WithComplexObject_Works()
    {
        // Arrange
        var responseObject = new { Id = 123, Name = "Test", Active = true };
        var responseText = "{ \"Id\": 123, \"Name\": \"Test\", \"Active\": true }";

        // Act
        var result = CreateObjectResponseResult(responseObject, responseText);

        // Assert
        Assert.AreEqual(responseObject, GetObjectProperty(result));
        Assert.AreEqual(responseText, GetTextProperty(result));
    }

    [TestMethod]
    public void ObjectResponseResult_Constructor_WithEmptyString_Works()
    {
        // Arrange
        var responseObject = "";
        var responseText = "";

        // Act
        var result = CreateObjectResponseResult(responseObject, responseText);

        // Assert
        Assert.AreEqual(responseObject, GetObjectProperty(result));
        Assert.AreEqual(responseText, GetTextProperty(result));
    }

    // Helper methods to work with the private struct
    private object CreateObjectResponseResult<T>(T responseObject, string responseText)
    {
        var type = typeof(TempStickClient).GetNestedType("ObjectResponseResult`1", BindingFlags.NonPublic)!.MakeGenericType(typeof(T));
        return Activator.CreateInstance(type, responseObject, responseText)!;
    }

    private object? GetObjectProperty(object result)
    {
        var property = result.GetType().GetProperty("Object");
        return property!.GetValue(result);
    }

    private string? GetTextProperty(object result)
    {
        var property = result.GetType().GetProperty("Text");
        return property!.GetValue(result) as string;
    }
}

[TestClass]
public class EdgeCaseTests
{
    [TestMethod]
    public void User_WithAllFieldsNull_DeserializesCorrectly()
    {
        // Arrange
        var json = """{"id":"required_id"}""";

        // Act
        var user = JsonSerializer.Deserialize<User>(json);

        // Assert
        Assert.IsNotNull(user);
        Assert.AreEqual("required_id", user.Id);
        Assert.IsNull(user.Email);
        Assert.IsNull(user.FirstName);
        Assert.IsNull(user.LastName);
        Assert.IsNull(user.Phone);
        Assert.IsNull(user.ContactEmail);
        Assert.IsNull(user.Address);
        Assert.IsNull(user.Address2);
        Assert.IsNull(user.City);
        Assert.IsNull(user.State);
        Assert.IsNull(user.PostalCode);
        Assert.IsNull(user.TemperatureScale);
        Assert.IsNull(user.Timezone);
    }

    [TestMethod]
    public void SensorData_WithAllFieldsNull_DeserializesCorrectly()
    {
        // Arrange
        var json = """{}""";

        // Act
        var sensorData = JsonSerializer.Deserialize<SensorData>(json);

        // Assert
        Assert.IsNotNull(sensorData);
        Assert.IsNull(sensorData.Version);
        Assert.IsNull(sensorData.SensorId);
        Assert.IsNull(sensorData.SensorName);
        Assert.IsNull(sensorData.SensorMacAddress);
        Assert.IsNull(sensorData.OwnerId);
        Assert.IsNull(sensorData.Type);
        Assert.IsNull(sensorData.AlertInterval);
        Assert.IsNull(sensorData.SendInterval);
        Assert.IsNull(sensorData.LastCheckin);
        Assert.IsNull(sensorData.NextCheckin);
        Assert.IsNull(sensorData.Ssid);
        Assert.IsNull(sensorData.Alerts);
        Assert.IsNull(sensorData.AlertTempBelow);
        Assert.IsNull(sensorData.AlertTempAbove);
        Assert.IsNull(sensorData.AlertHumidityBelow);
        Assert.IsNull(sensorData.AlertHumidityAbove);
        Assert.IsNull(sensorData.LastMessages);
    }

    [TestMethod]
    public void Reading_WithNullSensorTime_HandlesCorrectly()
    {
        // Arrange
        var json = """
        {
            "temperature": 25.0,
            "humidity": 50.0,
            "offline": "0",
            "sensor_time": null
        }
        """;

        // Act
        var reading = JsonSerializer.Deserialize<Reading>(json);

        // Assert
        Assert.IsNotNull(reading);
        Assert.AreEqual(25.0, reading.Temperature);
        Assert.AreEqual(50.0, reading.Humidity);
        Assert.IsFalse(reading.Offline);
        Assert.IsNull(reading.SensorTime);
    }

    [TestMethod]
    public void Message_WithAllNullStringFields_HandlesCorrectly()
    {
        // Arrange
        var json = """
        {
            "temperature": 20.0,
            "humidity": 40.0,
            "voltage": null,
            "RSSI": null,
            "time_to_connect": null,
            "sensor_time_utc": null
        }
        """;

        // Act
        var message = JsonSerializer.Deserialize<Message>(json);

        // Assert
        Assert.IsNotNull(message);
        Assert.AreEqual(20.0, message.Temperature);
        Assert.AreEqual(40.0, message.Humidity);
        Assert.IsNull(message.Voltage);
        Assert.IsNull(message.Rssi);
        Assert.IsNull(message.TimeToConnect);
        Assert.IsNull(message.SensorTimeUtc);
    }

    [TestMethod]
    public void BaseResponseMetadata_WithNullFields_HandlesCorrectly()
    {
        // Arrange
        var json = """
        {
            "type": null,
            "message": null
        }
        """;

        // Act
        var metadata = JsonSerializer.Deserialize<BaseResponseMetadata>(json);

        // Assert
        Assert.IsNotNull(metadata);
        Assert.IsNull(metadata.Type);
        Assert.IsNull(metadata.Message);
    }

    [TestMethod]
    public void SensorData_WithEmptyCollections_HandlesCorrectly()
    {
        // Arrange
        var json = """
        {
            "sensor_id": "TEST123",
            "alerts": [],
            "last_messages": []
        }
        """;

        // Act
        var sensorData = JsonSerializer.Deserialize<SensorData>(json);

        // Assert
        Assert.IsNotNull(sensorData);
        Assert.AreEqual("TEST123", sensorData.SensorId);
        Assert.IsNotNull(sensorData.Alerts);
        Assert.AreEqual(0, sensorData.Alerts.Count);
        Assert.IsNotNull(sensorData.LastMessages);
        Assert.AreEqual(0, sensorData.LastMessages.Count);
    }
    [TestMethod]
    public void SensorsData_WithEmptySensorsList_HandlesCorrectly()
    {
        // Arrange
        var json = """
        {
            "items": []
        }
        """;

        // Act
        var sensorsData = JsonSerializer.Deserialize<SensorsData>(json);

        // Assert
        Assert.IsNotNull(sensorsData);
        Assert.IsNotNull(sensorsData.Sensors);
        Assert.AreEqual(0, sensorsData.Sensors.Count);
    }

    [TestMethod]
    public void ReadingData_WithEmptyReadingsList_HandlesCorrectly()
    {
        // Arrange
        var json = """
        {
            "readings": []
        }
        """;

        // Act
        var readingData = JsonSerializer.Deserialize<ReadingData>(json);

        // Assert
        Assert.IsNotNull(readingData);
        Assert.IsNotNull(readingData.Readings);
        Assert.AreEqual(0, readingData.Readings.Count);
    }

    [TestMethod]
    public void User_WeeklyReportDay_WithInvalidValue_ThrowsJsonException()
    {
        // Arrange
        var json = """
        {
            "id": "user123",
            "weekly_report_day": "invalid"
        }
        """;

        // Act & Assert
        Assert.ThrowsException<JsonException>(() =>
        {
            JsonSerializer.Deserialize<User>(json);
        });
    }
    [TestMethod]
    public void User_WeeklyReportDay_WithZero_ResultsInNegativeValue()
    {
        // Arrange
        var json = """
        {
            "id": "user123",
            "weekly_report_day": "0"
        }
        """;

        // Act
        var user = JsonSerializer.Deserialize<User>(json);

        // Assert
        Assert.IsNotNull(user);
        Assert.AreEqual(-1, (int)user.WeeklyReportDay);
    }

    [TestMethod]
    public void User_WeeklyReportDay_WithEight_ResultsInSevenValue()
    {
        // Arrange
        var json = """
        {
            "id": "user123",
            "weekly_report_day": "8"
        }
        """;

        // Act
        var user = JsonSerializer.Deserialize<User>(json);

        // Assert
        Assert.IsNotNull(user);
        Assert.AreEqual(7, (int)user.WeeklyReportDay);
    }
}
