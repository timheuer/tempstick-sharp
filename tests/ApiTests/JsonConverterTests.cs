using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TempStick;

namespace ApiTests;

[TestClass]
public class JsonConverterTests
{
    [TestMethod]
    public void BooleanConverter_ReadString_One_ReturnsTrue()
    {
        // Arrange - Use actual User class which has BooleanConverter
        var json = """{"id":"test","use_local_timezone":"1"}""";

        // Act
        var result = JsonSerializer.Deserialize<User>(json);

        // Assert
        Assert.IsTrue(result!.UseLocalTimeZone);
    }

    [TestMethod]
    public void BooleanConverter_ReadString_Zero_ReturnsFalse()
    {
        // Arrange - Use actual User class
        var json = """{"id":"test","use_local_timezone":"0"}""";

        // Act
        var result = JsonSerializer.Deserialize<User>(json);

        // Assert
        Assert.IsFalse(result!.UseLocalTimeZone);
    }

    [TestMethod]
    public void BooleanConverter_ReadInt_One_ReturnsTrue()
    {
        // Arrange - Use actual User class  
        var json = """{"id":"test","use_local_timezone":1}""";

        // Act
        var result = JsonSerializer.Deserialize<User>(json);

        // Assert
        Assert.IsTrue(result!.UseLocalTimeZone);
    }
    [TestMethod]
    public void BooleanConverter_ReadInt_Zero_ReturnsFalse()
    {
        // Arrange - Use actual User class
        var json = """{"id":"test","use_local_timezone":0}""";

        // Act
        var result = JsonSerializer.Deserialize<User>(json);

        // Assert
        Assert.IsFalse(result!.UseLocalTimeZone);
    }

    [TestMethod]
    public void BooleanConverter_ReadInt_Two_ReturnsFalse()
    {
        // Arrange - Use actual User class
        var json = """{"id":"test","use_local_timezone":2}""";

        // Act
        var result = JsonSerializer.Deserialize<User>(json);

        // Assert
        Assert.IsFalse(result!.UseLocalTimeZone);
    }

    [TestMethod]
    public void BooleanConverter_ReadString_EmptyString_ReturnsFalse()
    {
        // Arrange - Use actual User class
        var json = """{"id":"test","use_local_timezone":""}""";        // Act
        var result = JsonSerializer.Deserialize<User>(json);

        // Assert
        Assert.IsFalse(result!.UseLocalTimeZone);
    }
    [TestMethod]
    public void BooleanConverter_WriteTrue_ReturnsStringOne()
    {
        // Arrange - Use actual User class
        var user = new User { Id = "test", UseLocalTimeZone = true };

        // Act
        var json = JsonSerializer.Serialize(user);

        // Assert
        Assert.IsTrue(json.Contains("\"1\""));
    }

    [TestMethod]
    public void BooleanConverter_WriteFalse_ReturnsStringZero()
    {
        // Arrange - Use actual User class
        var user = new User { Id = "test", UseLocalTimeZone = false };

        // Act
        var json = JsonSerializer.Serialize(user);        // Assert
        Assert.IsTrue(json.Contains("\"0\""));
    }

    [TestMethod]
    public void DayOfWeekConverter_ReadSunday_ReturnsCorrectValue()
    {
        // Arrange - Use actual User class
        var json = """{"id":"test","weekly_report_day":"1"}""";

        // Act
        var result = JsonSerializer.Deserialize<User>(json);

        // Assert
        Assert.AreEqual(DayOfWeek.Sunday, result!.WeeklyReportDay);
    }

    [TestMethod]
    public void DayOfWeekConverter_ReadMonday_ReturnsCorrectValue()
    {
        // Arrange - Use actual User class
        var json = """{"id":"test","weekly_report_day":"2"}""";

        // Act
        var result = JsonSerializer.Deserialize<User>(json);

        // Assert
        Assert.AreEqual(DayOfWeek.Monday, result!.WeeklyReportDay);
    }
    [TestMethod]
    public void DayOfWeekConverter_ReadTuesday_ReturnsCorrectValue()
    {
        // Arrange - Use actual User class
        var json = """{"id":"test","weekly_report_day":"3"}""";

        // Act
        var result = JsonSerializer.Deserialize<User>(json);

        // Assert
        Assert.AreEqual(DayOfWeek.Tuesday, result!.WeeklyReportDay);
    }

    [TestMethod]
    public void DayOfWeekConverter_ReadSaturday_ReturnsCorrectValue()
    {
        // Arrange - Use actual User class
        var json = """{"id":"test","weekly_report_day":"7"}""";

        // Act
        var result = JsonSerializer.Deserialize<User>(json);        // Assert
        Assert.AreEqual(DayOfWeek.Saturday, result!.WeeklyReportDay);
    }

    [TestMethod]
    public void DayOfWeekConverter_ReadInvalidValue_ThrowsJsonException()
    {
        // Arrange - Use actual User class
        var json = """{"id":"test","weekly_report_day":"invalid"}""";        // Act & Assert
        Assert.ThrowsException<JsonException>(() =>
        {
            JsonSerializer.Deserialize<User>(json);
        });
    }

    [TestMethod]
    public void DayOfWeekConverter_WriteMonday_ReturnsCorrectString()
    {
        // Arrange - Use actual User class
        var user = new User { Id = "test", WeeklyReportDay = DayOfWeek.Monday };        // Act
        var json = JsonSerializer.Serialize(user);

        // Assert
        Assert.IsTrue(json.Contains("\"2\""));
    }

    [TestMethod]
    public void DayOfWeekConverter_WriteSunday_ReturnsCorrectString()
    {
        // Arrange - Use actual User class
        var user = new User { Id = "test", WeeklyReportDay = DayOfWeek.Sunday };        // Act
        var json = JsonSerializer.Serialize(user);

        // Assert
        Assert.IsTrue(json.Contains("\"1\""));
    }

    [TestMethod]
    public void DayOfWeekConverter_WriteSaturday_ReturnsCorrectString()
    {
        // Arrange - Use actual User class
        var user = new User { Id = "test", WeeklyReportDay = DayOfWeek.Saturday };        // Act
        var json = JsonSerializer.Serialize(user);

        // Assert
        Assert.IsTrue(json.Contains("\"7\""));
    }
}
