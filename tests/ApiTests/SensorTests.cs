using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TempStick;

namespace ApiTests;

[TestClass]
public class SensorTests
{
    string API_KEY = string.Empty;
    string SENSOR_ID = string.Empty;
    TempStick.TempStickClient client;

    [TestInitialize]
    public void SetupTests()
    {
        var builder = new ConfigurationBuilder()
            .AddUserSecrets("5bcb7714-77a2-46aa-8937-fbcf172873c7")
            .AddEnvironmentVariables();

        IConfiguration config = builder.Build();

        // get api key from environment variable
        API_KEY = config["API_KEY"];
        SENSOR_ID = config["SENSOR_ID"];

        client = new TempStick.TempStickClient(API_KEY);
    }

    [TestMethod]
    public async Task Sensor_Has_Info()
    {
        var sensor = await client?.GetSensorAsync(SENSOR_ID);
        Assert.IsNotNull(sensor);
    }

    [TestMethod]
    public async Task Sensor_Has_Readings()
    {
        var reading = await client?.GetReadingsAsync(SENSOR_ID);
        Assert.IsTrue(reading.Type.ToLowerInvariant() == "success");
    }

    [TestMethod]
    public async Task Can_Retrieve_Sensors()
    {
        var sensors = await client.GetSensorsAsync();
        Assert.IsTrue(sensors.Type.ToLowerInvariant() == "success");
    }

    [TestMethod]
    public async Task Sensor_Has_Name()
    {
        var sensors = await client.GetSensorsAsync();
        var sensor = await client.GetSensorAsync(sensors.Data.Sensors.FirstOrDefault().SensorId);
        Console.WriteLine($"Sensor ID: {sensor.Data.SensorId}");
        Assert.IsNotNull(sensor.Data.SensorName);
    }

    [TestMethod]
    public async Task Api_Key_Required_Key_ctor()
    {
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            var client2 = new TempStickClient("");
        });
    }

    [TestMethod]
    public async Task Api_Key_Required_HttpClient_ctor()
    {
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            var http2 = new HttpClient();
            var client2 = new TempStickClient(http2);
        });
    }
}
