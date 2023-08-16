namespace TempStickSharp;
public static class Extensions
{
    public static double AsFahrenheit(this double tempInCelsius)
    {
        return (tempInCelsius * 9 / 5) + 32;
    }

    public static string AsSignalQuality(this double dbm)
    {
        var q = 2 * (dbm + 100);
        return $"{q}%";
    }
}
