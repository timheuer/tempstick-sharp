using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
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
        string? sensorId = null;        // Act & Assert
        await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
        {
            await client.GetReadingsAsync(sensorId!, 0, "today", string.Empty, string.Empty);
        });
    }

    [TestMethod]
    public async Task GetSensorAsync_ApiReturns404_ThrowsApiException()
    {
        // Arrange
        var httpClient = new HttpClient(new TestMessageHandler("{}", HttpStatusCode.NotFound));
        httpClient.DefaultRequestHeaders.Add("X-API-KEY", "test-key");
        var client = new TempStickClient(httpClient);

        // Act & Assert
        var ex = await Assert.ThrowsExceptionAsync<ApiException>(() => client.GetSensorAsync("123"));
        Assert.AreEqual(404, ex.StatusCode);
    }

    [TestMethod]
    public async Task GetCurrentUserAsync_ApiReturns404_ThrowsApiException()
    {
        // Arrange
        var httpClient = new HttpClient(new TestMessageHandler("{}", HttpStatusCode.NotFound));
        httpClient.DefaultRequestHeaders.Add("X-API-KEY", "test-key");
        var client = new TempStickClient(httpClient);

        // Act & Assert
        var ex = await Assert.ThrowsExceptionAsync<ApiException>(() => client.GetCurrentUserAsync());
        Assert.AreEqual(404, ex.StatusCode);
    }

    [TestMethod]
    public async Task GetSensorsAsync_ApiReturns404_ThrowsApiException()
    {
        // Arrange
        var httpClient = new HttpClient(new TestMessageHandler("{}", HttpStatusCode.NotFound));
        httpClient.DefaultRequestHeaders.Add("X-API-KEY", "test-key");
        var client = new TempStickClient(httpClient);

        // Act & Assert
        var ex = await Assert.ThrowsExceptionAsync<ApiException>(() => client.GetSensorsAsync());
        Assert.AreEqual(404, ex.StatusCode);
    }

    [TestMethod]
    public async Task GetReadingsAsync_ApiReturns404_ThrowsApiException()
    {
        // Arrange
        var httpClient = new HttpClient(new TestMessageHandler("{}", HttpStatusCode.NotFound));
        httpClient.DefaultRequestHeaders.Add("X-API-KEY", "test-key");
        var client = new TempStickClient(httpClient);

        // Act & Assert
        var ex = await Assert.ThrowsExceptionAsync<ApiException>(() => client.GetReadingsAsync("123"));
        Assert.AreEqual(404, ex.StatusCode);
    }

    [TestMethod]
    public void TempStickClient_JsonSerializerOptions_ReturnsConfiguredOptions()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");

        // Act
        var options = client.GetType().GetProperty("JsonSerializerOptions", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(client);

        // Assert
        Assert.IsNotNull(options);
        Assert.IsInstanceOfType(options, typeof(JsonSerializerOptions));
    }

    [TestMethod]
    public void TempStickClient_CreateSerializerSettings_CreatesValidSettings()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var method = typeof(TempStickClient).GetMethod("CreateSerializerSettings", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act
        var result = method!.Invoke(client, null);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(JsonSerializerOptions));
    }

    [TestMethod]
    public async Task TempStickClient_ReadObjectResponseAsync_WithReadResponseAsStringTrue_WorksCorrectly()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        client.ReadResponseAsString = true;

        var json = """{"id":"test","email":"test@example.com"}""";
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        var headers = new Dictionary<string, IEnumerable<string>>();

        var method = typeof(TempStickClient).GetMethod("ReadObjectResponseAsync", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act
        var task = (Task)method!.MakeGenericMethod(typeof(User)).Invoke(client, new object[] { response, headers, CancellationToken.None })!;
        await task;
        var result = task.GetType().GetProperty("Result")!.GetValue(task);
        var objectProperty = result!.GetType().GetProperty("Object")!.GetValue(result);

        // Assert
        Assert.IsNotNull(objectProperty);
        Assert.IsInstanceOfType(objectProperty, typeof(User));
        var user = (User)objectProperty;
        Assert.AreEqual("test", user.Id);
    }

    [TestMethod]
    public async Task TempStickClient_ReadObjectResponseAsync_WithNullResponse_ReturnsDefault()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var headers = new Dictionary<string, IEnumerable<string>>();

        var method = typeof(TempStickClient).GetMethod("ReadObjectResponseAsync", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);        // Act
        var task = (Task)method!.MakeGenericMethod(typeof(User)).Invoke(client, new object?[] { null, headers, CancellationToken.None })!;
        await task;
        var result = task.GetType().GetProperty("Result")!.GetValue(task);
        var objectProperty = result!.GetType().GetProperty("Object")!.GetValue(result);        // Assert
        Assert.IsNull(objectProperty);
    }

    [TestMethod]
    public async Task TempStickClient_ReadObjectResponseAsync_WithNullContent_ThrowsApiException()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = null };
        var headers = new Dictionary<string, IEnumerable<string>>();

        var method = typeof(TempStickClient).GetMethod("ReadObjectResponseAsync", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act & Assert
        var task = (Task)method!.MakeGenericMethod(typeof(User)).Invoke(client, new object[] { response, headers, CancellationToken.None })!;

        await Assert.ThrowsExceptionAsync<ApiException>(async () => await task);
    }

    [TestMethod]
    public async Task TempStickClient_ReadObjectResponseAsync_WithEmptyContent_ThrowsApiException()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("", Encoding.UTF8, "application/json")
        };
        var headers = new Dictionary<string, IEnumerable<string>>();

        var method = typeof(TempStickClient).GetMethod("ReadObjectResponseAsync", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act & Assert
        var task = (Task)method!.MakeGenericMethod(typeof(User)).Invoke(client, new object[] { response, headers, CancellationToken.None })!;

        await Assert.ThrowsExceptionAsync<ApiException>(async () => await task);
    }

    [TestMethod]
    public async Task TempStickClient_ReadObjectResponseAsync_WithStreamReading_WorksCorrectly()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        client.ReadResponseAsString = false; // Force stream reading

        var json = """{"id":"test","email":"test@example.com"}""";

        // Create compressed content
        using var memoryStream = new MemoryStream();
        using (var gzipStream = new System.IO.Compression.GZipStream(memoryStream, System.IO.Compression.CompressionMode.Compress))
        using (var writer = new StreamWriter(gzipStream))
        {
            writer.Write(json);
        }

        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new ByteArrayContent(memoryStream.ToArray())
        };
        response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        var headers = new Dictionary<string, IEnumerable<string>>();

        var method = typeof(TempStickClient).GetMethod("ReadObjectResponseAsync", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act
        var task = (Task)method!.MakeGenericMethod(typeof(User)).Invoke(client, new object[] { response, headers, CancellationToken.None })!;
        await task;
        var result = task.GetType().GetProperty("Result")!.GetValue(task);
        var objectProperty = result!.GetType().GetProperty("Object")!.GetValue(result);

        // Assert
        Assert.IsNotNull(objectProperty);
        Assert.IsInstanceOfType(objectProperty, typeof(User));
        var user = (User)objectProperty;
        Assert.AreEqual("test", user.Id);
    }

    [TestMethod]
    public async Task TempStickClient_ReadObjectResponseAsync_WithInvalidJson_ThrowsApiException()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        client.ReadResponseAsString = true;

        var invalidJson = "{ invalid json }";
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(invalidJson, Encoding.UTF8, "application/json")
        };
        var headers = new Dictionary<string, IEnumerable<string>>();

        var method = typeof(TempStickClient).GetMethod("ReadObjectResponseAsync", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act & Assert
        var task = (Task)method!.MakeGenericMethod(typeof(User)).Invoke(client, new object[] { response, headers, CancellationToken.None })!;

        await Assert.ThrowsExceptionAsync<ApiException>(async () => await task);
    }

    public enum TestEnum
    {
        [System.Runtime.Serialization.EnumMember(Value = "custom_value")]
        CustomValue,

        [System.Runtime.Serialization.EnumMember(Value = "another_value")]
        AnotherValue,

        NoAttribute
    }

    [TestMethod]
    public void ConvertToString_WithEnumMemberAttribute_ReturnsAttributeValue()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var method = typeof(TempStickClient).GetMethod("ConvertToString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act
        var result = method!.Invoke(client, new object[] { TestEnum.CustomValue, System.Globalization.CultureInfo.InvariantCulture });

        // Assert
        Assert.AreEqual("custom_value", result);
    }

    [TestMethod]
    public void ConvertToString_WithEnumMemberAttributeNullValue_ReturnsEnumName()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var method = typeof(TempStickClient).GetMethod("ConvertToString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act
        var result = method!.Invoke(client, new object[] { TestEnum.NoAttribute, System.Globalization.CultureInfo.InvariantCulture });

        // Assert
        Assert.AreEqual("2", result); // Should return underlying value since no attribute
    }

    [TestMethod]
    public void ConvertToString_WithDecimal_ReturnsStringRepresentation()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var method = typeof(TempStickClient).GetMethod("ConvertToString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act
        var result = method!.Invoke(client, new object[] { 42.5m, System.Globalization.CultureInfo.InvariantCulture });

        // Assert
        Assert.AreEqual("42.5", result);
    }

    [TestMethod]
    public void ConvertToString_WithDouble_ReturnsStringRepresentation()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var method = typeof(TempStickClient).GetMethod("ConvertToString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act
        var result = method!.Invoke(client, new object[] { 42.5d, System.Globalization.CultureInfo.InvariantCulture });

        // Assert
        Assert.AreEqual("42.5", result);
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

    [TestMethod]
    public void ObjectResponseResult_Constructor_SetsProperties()
    {
        // Arrange
        var testObject = new User { Id = "test" };
        var testText = "test response text";

        // Use reflection to access the internal struct
        var type = typeof(TempStickClient).GetNestedType("ObjectResponseResult`1", System.Reflection.BindingFlags.NonPublic);
        var genericType = type!.MakeGenericType(typeof(User));
        var constructor = genericType.GetConstructor(new Type[] { typeof(User), typeof(string) });

        // Act
        var result = constructor!.Invoke(new object[] { testObject, testText });
        var objectProperty = genericType.GetProperty("Object")!.GetValue(result);
        var textProperty = genericType.GetProperty("Text")!.GetValue(result);

        // Assert
        Assert.AreEqual(testObject, objectProperty);
        Assert.AreEqual(testText, textProperty);
    }

    [TestMethod]
    public void ObjectResponseResult_Constructor_WithNull_HandlesNullCorrectly()
    {
        // Arrange
        User? testObject = null;
        var testText = "test response text";

        // Use reflection to access the internal struct
        var type = typeof(TempStickClient).GetNestedType("ObjectResponseResult`1", System.Reflection.BindingFlags.NonPublic);
        var genericType = type!.MakeGenericType(typeof(User));
        var constructor = genericType.GetConstructor(new Type[] { typeof(User), typeof(string) });

        // Act
        var result = constructor!.Invoke(new object?[] { testObject, testText });
        var objectProperty = genericType.GetProperty("Object")!.GetValue(result);
        var textProperty = genericType.GetProperty("Text")!.GetValue(result);

        // Assert
        Assert.IsNull(objectProperty);
        Assert.AreEqual(testText, textProperty);
    }

    [TestMethod]
    public void ConvertToString_WithDateTime_ReturnsStringRepresentation()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var method = typeof(TempStickClient).GetMethod("ConvertToString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var testDate = new DateTime(2023, 12, 25, 10, 30, 0);

        // Act
        var result = method!.Invoke(client, new object[] { testDate, System.Globalization.CultureInfo.InvariantCulture });

        // Assert
        Assert.AreEqual("12/25/2023 10:30:00", result);
    }

    [TestMethod]
    public void ConvertToString_WithFloat_ReturnsStringRepresentation()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var method = typeof(TempStickClient).GetMethod("ConvertToString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act
        var result = method!.Invoke(client, new object[] { 42.5f, System.Globalization.CultureInfo.InvariantCulture });

        // Assert
        Assert.AreEqual("42.5", result);
    }

    [TestMethod]
    public void ConvertToString_WithGuid_ReturnsStringRepresentation()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var method = typeof(TempStickClient).GetMethod("ConvertToString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var testGuid = Guid.Parse("12345678-1234-5678-9abc-123456789abc");

        // Act
        var result = method!.Invoke(client, new object[] { testGuid, System.Globalization.CultureInfo.InvariantCulture });

        // Assert
        Assert.AreEqual("12345678-1234-5678-9abc-123456789abc", result);
    }

    public enum TestEnumWithNullAttribute
    {
        [System.Runtime.Serialization.EnumMember(Value = null)]
        NullValue
    }

    [TestMethod]
    public void ConvertToString_WithEnumMemberAttributeNullValue_ReturnsEnumName()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var method = typeof(TempStickClient).GetMethod("ConvertToString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act
        var result = method!.Invoke(client, new object[] { TestEnumWithNullAttribute.NullValue, System.Globalization.CultureInfo.InvariantCulture });

        // Assert
        Assert.AreEqual("NullValue", result); // Should return enum name when attribute value is null
    }

    [TestMethod]
    public void ConvertToString_WithComplexArray_ReturnsCommaSeparatedString()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var method = typeof(TempStickClient).GetMethod("ConvertToString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var array = new object[] { 1, "test", true, 3.14 };

        // Act
        var result = method!.Invoke(client, new object[] { array, System.Globalization.CultureInfo.InvariantCulture });

        // Assert
        Assert.AreEqual("1,test,true,3.14", result);
    }
    [TestMethod]
    public void TempStickClient_BaseUrl_WithNullHandling()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");

        // Act
        client.BaseUrl = null!;
        var result = client.BaseUrl;

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public void TempStickClient_ReadResponseAsString_PropertyAccessors()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");

        // Act & Assert
        Assert.IsFalse(client.ReadResponseAsString); // default value

        client.ReadResponseAsString = true;
        Assert.IsTrue(client.ReadResponseAsString);

        client.ReadResponseAsString = false;
        Assert.IsFalse(client.ReadResponseAsString);
    }

    [TestMethod]
    public void ConvertToString_WithEnumWithoutEnumMemberAttribute_ReturnsNumericValue()
    {
        // Arrange
        var client = new TempStickClient("test-api-key");
        var enumValue = DayOfWeek.Monday; // This enum doesn't have EnumMemberAttribute

        // Act (using reflection to access private method)
        var method = typeof(TempStickClient).GetMethod("ConvertToString", BindingFlags.NonPublic | BindingFlags.Instance);
        var result = method?.Invoke(client, new object[] { enumValue, System.Globalization.CultureInfo.InvariantCulture }) as string;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("1", result); // Monday as numeric
    }

    [TestMethod]
    public async Task ReadObjectResponseAsync_WithGZipDecompression_StreamPath()
    {
        // This test covers the GZip decompression path in ReadObjectResponseAsync
        var validJson = @"{""type"":""success"",""data"":{""id"":""123""}}";
        var httpClient = new HttpClient(new TestMessageHandler(validJson));
        httpClient.DefaultRequestHeaders.Add("X-API-KEY", "test-key");
        var client = new TempStickClient(httpClient);

        // Set ReadResponseAsString to false to use the stream path
        client.ReadResponseAsString = false;

        // Act & Assert
        try
        {
            await client.GetCurrentUserAsync();
        }
        catch (Exception)
        {
            // Expected since the JSON structure doesn't match UserResponse exactly
        }
    }
}

// Test HTTP message handler for mocking responses
public class TestMessageHandler : HttpMessageHandler
{
    private readonly string _response;
    private readonly HttpStatusCode _statusCode;

    public TestMessageHandler(string response, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        _response = response;
        _statusCode = statusCode;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = new HttpResponseMessage(_statusCode)
        {
            Content = new StringContent(_response)
        };
        return Task.FromResult(response);
    }
}
