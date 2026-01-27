using Microsoft.VisualStudio.TestTools.UnitTesting;
using TempStick;

namespace ApiTests;

[TestClass]
public class ExtensionTests
{
    [TestMethod]
    public void AsFahrenheit_ConvertsFromCelsius_ReturnsCorrectValue()
    {
        // Arrange
        double celsius = 0.0;
        double expectedFahrenheit = 32.0;

        // Act
        double result = celsius.AsFahrenheit();

        // Assert
        Assert.AreEqual(expectedFahrenheit, result, 0.01);
    }

    [TestMethod]
    public void AsFahrenheit_ConvertsFreezing_ReturnsThirtyTwo()
    {
        // Arrange
        double celsius = 0.0;

        // Act
        double result = celsius.AsFahrenheit();

        // Assert
        Assert.AreEqual(32.0, result, 0.01);
    }

    [TestMethod]
    public void AsFahrenheit_ConvertsBoiling_ReturnsTwoTwelve()
    {
        // Arrange
        double celsius = 100.0;

        // Act
        double result = celsius.AsFahrenheit();

        // Assert
        Assert.AreEqual(212.0, result, 0.01);
    }

    [TestMethod]
    public void AsFahrenheit_ConvertsNegativeValue_ReturnsCorrectNegative()
    {
        // Arrange
        double celsius = -40.0;

        // Act
        double result = celsius.AsFahrenheit();

        // Assert
        Assert.AreEqual(-40.0, result, 0.01);
    }

    [TestMethod]
    public void AsFahrenheit_ConvertsRoomTemperature_ReturnsCorrectValue()
    {
        // Arrange
        double celsius = 20.0;
        double expectedFahrenheit = 68.0;

        // Act
        double result = celsius.AsFahrenheit();

        // Assert
        Assert.AreEqual(expectedFahrenheit, result, 0.01);
    }

    [TestMethod]
    public void AsSignalQuality_WithGoodSignal_ReturnsHighPercentage()
    {
        // Arrange
        double dbm = -30.0;
        string expected = "140%";

        // Act
        string result = dbm.AsSignalQuality();

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void AsSignalQuality_WithPoorSignal_ReturnsLowPercentage()
    {
        // Arrange
        double dbm = -80.0;
        string expected = "40%";

        // Act
        string result = dbm.AsSignalQuality();

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void AsSignalQuality_WithExtremelyWeakSignal_ReturnsZeroPercent()
    {
        // Arrange
        double dbm = -100.0;
        string expected = "0%";

        // Act
        string result = dbm.AsSignalQuality();

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void AsSignalQuality_WithVeryStrongSignal_ReturnsOverHundredPercent()
    {
        // Arrange
        double dbm = -10.0;
        string expected = "180%";

        // Act
        string result = dbm.AsSignalQuality();

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void AsSignalQuality_WithNegativeFiftyDbm_ReturnsFiftyPercent()
    {
        // Arrange
        double dbm = -75.0;
        string expected = "50%";

        // Act
        string result = dbm.AsSignalQuality();

        // Assert
        Assert.AreEqual(expected, result);
    }
}
