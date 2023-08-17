using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;
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
        var jsonString = @"{""id"":""12345"",""weekly_report_day"":""3""}"; // 3 represents Tuesday for the API
        var user = JsonSerializer.Deserialize<User>(jsonString);
        Assert.AreEqual(DayOfWeek.Tuesday, user.WeeklyReportDay);
    }

    [TestMethod]
    public async Task Boolean_Converter_String_or_Int()
    {
        var jsonString = @"{""id"":""12345"",""chart_fill"":""1"",""use_local_timezone"":0}"; // 3 represents Tuesday for the API
        var user = JsonSerializer.Deserialize<User>(jsonString);

        Assert.IsFalse(user.UseLocalTimeZone);
        Assert.IsTrue(user.ChartFill);
    }
}
