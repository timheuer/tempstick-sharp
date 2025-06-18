using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TempStick;

namespace ApiTests;

[TestClass]
public class TempStickClientUnitTests
{
    [TestMethod]
    public void TempStickClient_Constructor_WithValidApiKey_SetsProperties()
    {
        // Arrange
        var apiKey = "test-api-key";

        // Act
        var client = new TempStickClient(apiKey);

        // Assert
        Assert.IsNotNull(client);
        Assert.AreEqual("https://tempstickapi.com/api/v1", client.BaseUrl);
    }

    [TestMethod]
    public void TempStickClient_Constructor_WithNullApiKey_ThrowsArgumentNullException()
    {
        // Arrange
        string? apiKey = null;

        // Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() => new TempStickClient(apiKey!));
    }

    [TestMethod]
    public void TempStickClient_Constructor_WithEmptyApiKey_ThrowsArgumentNullException()
    {
        // Arrange
        var apiKey = "";

        // Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() => new TempStickClient(apiKey));
    }

    [TestMethod]
    public void TempStickClient_Constructor_WithWhitespaceApiKey_ThrowsArgumentNullException()
    {
        // Arrange
        var apiKey = "   ";

        // Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() => new TempStickClient(apiKey));
    }

    [TestMethod]
    public void TempStickClient_Constructor_WithHttpClient_WithoutApiKey_ThrowsArgumentNullException()
    {
        // Arrange
        var httpClient = new HttpClient();

        // Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() => new TempStickClient(httpClient));
    }

    [TestMethod]
    public void TempStickClient_Constructor_WithHttpClient_WithApiKey_Works()
    {
        // Arrange
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("X-API-KEY", "test-api-key");

        // Act
        var client = new TempStickClient(httpClient);

        // Assert
        Assert.IsNotNull(client);
        Assert.AreEqual("https://tempstickapi.com/api/v1", client.BaseUrl);
    }

    [TestMethod]
    public void TempStickClient_Constructor_WithNullHttpClient_ThrowsArgumentNullException()
    {
        // Arrange
        HttpClient? httpClient = null;

        // Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() => new TempStickClient(httpClient!));
    }

    [TestMethod]
    public void TempStickClient_BaseUrl_CanBeChanged()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var newBaseUrl = "https://custom-api.example.com/v2";

        // Act
        client.BaseUrl = newBaseUrl;

        // Assert
        Assert.AreEqual(newBaseUrl, client.BaseUrl);
    }

    [TestMethod]
    public void TempStickClient_BaseUrl_CanBeSetToNull()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");

        // Act
        client.BaseUrl = null!;

        // Assert
        Assert.IsNull(client.BaseUrl);
    }

    [TestMethod]
    public void TempStickClient_ReadResponseAsString_CanBeSet()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");

        // Act
        client.ReadResponseAsString = true;

        // Assert
        Assert.IsTrue(client.ReadResponseAsString);
    }

    [TestMethod]
    public void TempStickClient_ReadResponseAsString_DefaultsToFalse()
    {
        // Arrange & Act
        var client = new TempStickClient("test-api-key");

        // Assert
        Assert.IsFalse(client.ReadResponseAsString);
    }

    [TestMethod]
    public async Task TempStickClient_GetSensorAsync_WithNullSensorId_ThrowsArgumentNullException()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        string? sensorId = null;

        // Act & Assert
        await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
        {
            await client.GetSensorAsync(sensorId!);
        });
    }

    [TestMethod]
    public async Task TempStickClient_GetReadingsAsync_WithNullSensorId_ThrowsArgumentNullException()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        string? sensorId = null;

        // Act & Assert
        await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
        {
            await client.GetReadingsAsync(sensorId!);
        });
    }

    [TestMethod]
    public async Task TempStickClient_GetReadingsAsync_WithParameters_WithNullSensorId_ThrowsArgumentNullException()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        string? sensorId = null;

        // Act & Assert
        await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
        {
            await client.GetReadingsAsync(sensorId!, 0, "today", null, null);
        });
    }
}

[TestClass]
public class TempStickClientConversionTests
{
    [TestMethod]
    public void ConvertToString_WithNull_ReturnsEmptyString()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var method = typeof(TempStickClient).GetMethod("ConvertToString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act
        var result = method!.Invoke(client, new object[] { null!, System.Globalization.CultureInfo.InvariantCulture });

        // Assert
        Assert.AreEqual("", result);
    }

    [TestMethod]
    public void ConvertToString_WithBooleanTrue_ReturnsLowerCaseTrue()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var method = typeof(TempStickClient).GetMethod("ConvertToString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act
        var result = method!.Invoke(client, new object[] { true, System.Globalization.CultureInfo.InvariantCulture });

        // Assert
        Assert.AreEqual("true", result);
    }

    [TestMethod]
    public void ConvertToString_WithBooleanFalse_ReturnsLowerCaseFalse()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var method = typeof(TempStickClient).GetMethod("ConvertToString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act
        var result = method!.Invoke(client, new object[] { false, System.Globalization.CultureInfo.InvariantCulture });

        // Assert
        Assert.AreEqual("false", result);
    }

    [TestMethod]
    public void ConvertToString_WithByteArray_ReturnsBase64String()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var method = typeof(TempStickClient).GetMethod("ConvertToString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var bytes = Encoding.UTF8.GetBytes("test");

        // Act
        var result = method!.Invoke(client, new object[] { bytes, System.Globalization.CultureInfo.InvariantCulture });

        // Assert
        var expected = Convert.ToBase64String(bytes);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void ConvertToString_WithArray_ReturnsCommaSeparatedString()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var method = typeof(TempStickClient).GetMethod("ConvertToString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var array = new string[] { "item1", "item2", "item3" };

        // Act
        var result = method!.Invoke(client, new object[] { array, System.Globalization.CultureInfo.InvariantCulture });

        // Assert
        Assert.AreEqual("item1,item2,item3", result);
    }

    [TestMethod]
    public void ConvertToString_WithInteger_ReturnsStringRepresentation()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var method = typeof(TempStickClient).GetMethod("ConvertToString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act
        var result = method!.Invoke(client, new object[] { 42, System.Globalization.CultureInfo.InvariantCulture });

        // Assert
        Assert.AreEqual("42", result);
    }

    [TestMethod]
    public void ConvertToString_WithString_ReturnsString()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var method = typeof(TempStickClient).GetMethod("ConvertToString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act
        var result = method!.Invoke(client, new object[] { "test string", System.Globalization.CultureInfo.InvariantCulture });

        // Assert
        Assert.AreEqual("test string", result);
    }
}
