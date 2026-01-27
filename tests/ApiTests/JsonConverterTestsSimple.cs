using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TempStick;

namespace ApiTests;

[TestClass]
public class JsonConverterSimpleTests
{
    [TestMethod]
    public void BooleanConverter_InstanceCanBeCreated()
    {
        // Arrange & Act
        var converter = new BooleanConverter();

        // Assert
        Assert.IsNotNull(converter);
    }

    [TestMethod]
    public void DayOfWeekConverter_InstanceCanBeCreated()
    {
        // Arrange & Act
        var converter = new DayOfWeekConverter();

        // Assert
        Assert.IsNotNull(converter);
    }

    [TestMethod]
    public void BooleanConverter_CanBeAddedToJsonOptions()
    {
        // Arrange
        var options = new JsonSerializerOptions();

        // Act
        options.Converters.Add(new BooleanConverter());

        // Assert
        Assert.AreEqual(1, options.Converters.Count);
        Assert.IsInstanceOfType(options.Converters[0], typeof(BooleanConverter));
    }

    [TestMethod]
    public void DayOfWeekConverter_CanBeAddedToJsonOptions()
    {
        // Arrange
        var options = new JsonSerializerOptions();

        // Act
        options.Converters.Add(new DayOfWeekConverter());

        // Assert
        Assert.AreEqual(1, options.Converters.Count);
        Assert.IsInstanceOfType(options.Converters[0], typeof(DayOfWeekConverter));
    }

    [TestMethod]
    public void BothConverters_CanBeUsedTogether()
    {
        // Arrange
        var options = new JsonSerializerOptions();

        // Act
        options.Converters.Add(new BooleanConverter());
        options.Converters.Add(new DayOfWeekConverter());

        // Assert
        Assert.AreEqual(2, options.Converters.Count);
        Assert.IsInstanceOfType(options.Converters[0], typeof(BooleanConverter));
        Assert.IsInstanceOfType(options.Converters[1], typeof(DayOfWeekConverter));
    }
}
