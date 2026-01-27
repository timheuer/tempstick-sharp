using Microsoft.VisualStudio.TestTools.UnitTesting;
using TempStick;

namespace ApiTests;

[TestClass]
public class ApiExceptionTests
{
    [TestMethod]
    public void ApiException_Constructor_SetsAllProperties()
    {
        // Arrange
        var message = "Test error message";
        var statusCode = 400;
        var response = "Error response";
        var headers = new Dictionary<string, IEnumerable<string>>
        {
            ["Content-Type"] = new[] { "application/json" }
        };
        var innerException = new InvalidOperationException("Inner exception");

        // Act
        var apiException = new ApiException(message, statusCode, response, headers, innerException);

        // Assert
        Assert.AreEqual(statusCode, apiException.StatusCode);
        Assert.AreEqual(response, apiException.Response);
        Assert.AreEqual(headers, apiException.Headers);
        Assert.AreEqual(innerException, apiException.InnerException);
        Assert.IsTrue(apiException.Message.Contains(message));
        Assert.IsTrue(apiException.Message.Contains(statusCode.ToString()));
        Assert.IsTrue(apiException.Message.Contains(response));
    }

    [TestMethod]
    public void ApiException_Constructor_WithNullResponse_HandlesNull()
    {
        // Arrange
        var message = "Test error message";
        var statusCode = 500;
        string? response = null;
        var headers = new Dictionary<string, IEnumerable<string>>();
        var innerException = new Exception("Inner exception");

        // Act
        var apiException = new ApiException(message, statusCode, response!, headers, innerException);

        // Assert
        Assert.AreEqual(statusCode, apiException.StatusCode);
        Assert.AreEqual(response, apiException.Response);
        Assert.IsTrue(apiException.Message.Contains("(null)"));
    }

    [TestMethod]
    public void ApiException_Constructor_WithLongResponse_TruncatesResponse()
    {
        // Arrange
        var message = "Test error";
        var statusCode = 404;
        var longResponse = new string('x', 1000); // Create a 1000 character response
        var headers = new Dictionary<string, IEnumerable<string>>();

        // Act
        var apiException = new ApiException(message, statusCode, longResponse, headers, null!);

        // Assert
        Assert.AreEqual(statusCode, apiException.StatusCode);
        Assert.AreEqual(longResponse, apiException.Response);
        // The message should contain the truncated response (512 chars max)
        Assert.IsTrue(apiException.Message.Contains(longResponse.Substring(0, 512)));
    }

    [TestMethod]
    public void ApiException_ToString_ReturnsFormattedString()
    {
        // Arrange
        var message = "API Error";
        var statusCode = 401;
        var response = "Unauthorized";
        var headers = new Dictionary<string, IEnumerable<string>>();

        var apiException = new ApiException(message, statusCode, response, headers, null!);

        // Act
        var result = apiException.ToString();

        // Assert
        Assert.IsTrue(result.Contains("HTTP ReadingResponse:"));
        Assert.IsTrue(result.Contains(response));
    }

    [TestMethod]
    public void ApiException_WithEmptyHeaders_WorksCorrectly()
    {
        // Arrange
        var message = "Empty headers test";
        var statusCode = 503;
        var response = "Service unavailable";
        var headers = new Dictionary<string, IEnumerable<string>>();

        // Act
        var apiException = new ApiException(message, statusCode, response, headers, null!);

        // Assert
        Assert.AreEqual(0, apiException.Headers.Count);
        Assert.AreEqual(statusCode, apiException.StatusCode);
        Assert.AreEqual(response, apiException.Response);
    }

    [TestMethod]
    public void ApiException_WithMultipleHeaders_PreservesAllHeaders()
    {
        // Arrange
        var headers = new Dictionary<string, IEnumerable<string>>
        {
            ["Content-Type"] = new[] { "application/json" },
            ["Authorization"] = new[] { "Bearer token123" },
            ["X-Custom-Header"] = new[] { "value1", "value2" }
        };

        // Act
        var apiException = new ApiException("Test", 400, "Bad request", headers, null!);

        // Assert
        Assert.AreEqual(3, apiException.Headers.Count);
        Assert.IsTrue(apiException.Headers.ContainsKey("Content-Type"));
        Assert.IsTrue(apiException.Headers.ContainsKey("Authorization"));
        Assert.IsTrue(apiException.Headers.ContainsKey("X-Custom-Header"));
        Assert.AreEqual(2, apiException.Headers["X-Custom-Header"].Count());
    }
}

[TestClass]
public class ApiExceptionGenericTests
{
    [TestMethod]
    public void ApiExceptionGeneric_Constructor_SetsAllProperties()
    {
        // Arrange
        var message = "Generic API error";
        var statusCode = 422;
        var response = "Validation failed";
        var headers = new Dictionary<string, IEnumerable<string>>
        {
            ["Content-Type"] = new[] { "application/json" }
        };
        var result = new TestResult { Id = 123, Name = "Test Result" };
        var innerException = new ArgumentException("Inner exception");

        // Act
        var apiException = new ApiException<TestResult>(message, statusCode, response, headers, result, innerException);

        // Assert
        Assert.AreEqual(statusCode, apiException.StatusCode);
        Assert.AreEqual(response, apiException.Response);
        Assert.AreEqual(headers, apiException.Headers);
        Assert.AreEqual(result, apiException.Result);
        Assert.AreEqual(innerException, apiException.InnerException);
        Assert.IsTrue(apiException.Message.Contains(message));
    }

    [TestMethod]
    public void ApiExceptionGeneric_Constructor_WithNullResult_HandlesNull()
    {
        // Arrange
        var message = "Null result test";
        var statusCode = 204;
        var response = "No content";
        var headers = new Dictionary<string, IEnumerable<string>>();
        TestResult? result = null;

        // Act
        var apiException = new ApiException<TestResult>(message, statusCode, response, headers, result!, null!);

        // Assert
        Assert.AreEqual(statusCode, apiException.StatusCode);
        Assert.AreEqual(response, apiException.Response);
        Assert.IsNull(apiException.Result);
    }

    [TestMethod]
    public void ApiExceptionGeneric_InheritsFromApiException()
    {
        // Arrange
        var apiException = new ApiException<string>("Test", 500, "Error", new Dictionary<string, IEnumerable<string>>(), "result", null!);

        // Act & Assert
        Assert.IsInstanceOfType(apiException, typeof(ApiException));
        Assert.IsInstanceOfType(apiException, typeof(ApiException<string>));
    }

    private class TestResult
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
