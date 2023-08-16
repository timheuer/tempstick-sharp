using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TempStick;

namespace ApiTests;

[TestClass]
public class UserTests
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

        client = new TempStickClient(API_KEY);
    }

    [TestMethod]
    public async Task User_Has_id()
    {
        var user = await client?.GetCurrentUserAsync();
        Assert.IsNotNull(user.User.Id);
        Console.Write(user.User.Id);
    }

    [TestMethod]
    public async Task DayOfWeek_Correct()
    {
        var user = await client?.GetCurrentUserAsync();
        Assert.AreEqual(DayOfWeek.Tuesday, user.User.WeeklyReportDay);
    }
}
