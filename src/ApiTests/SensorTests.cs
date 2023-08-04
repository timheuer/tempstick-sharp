using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            .AddUserSecrets("5bcb7714-77a2-46aa-8937-fbcf172873c7");

        IConfiguration config = builder.Build();

        // get api key from environment variable
        API_KEY = config["API_KEY"];
        SENSOR_ID = config["SENSOR_ID"];


        HttpClient http = new HttpClient();
        http.DefaultRequestHeaders.Add("X-API-KEY", API_KEY);
        
        client = new TempStick.TempStickClient(http);
    }

    [TestMethod]
    public async Task GetSensor()
    {
        var sensor = await client.GetSensorAsync(SENSOR_ID);
        Assert.IsNotNull(sensor);
    }

    [TestMethod]
    public async Task Sensor_Has_Readings()
    {
        var reading = await client.GetReadingsAsync(SENSOR_ID);
        Assert.IsTrue(reading.Type.ToLowerInvariant() == "success");
    }
}
