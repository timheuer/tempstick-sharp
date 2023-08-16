[![Build](https://github.com/timheuer/tempstick-sharp/actions/workflows/build.yaml/badge.svg)](https://github.com/timheuer/tempstick-sharp/actions/workflows/build.yaml)
[![GitHub last commit](https://img.shields.io/github/last-commit/timheuer/tempstick-sharp)](https://github.com/timheuer/tempstick-sharp/)
[![Nuget](https://img.shields.io/nuget/dt/TempStickSharp?label=NuGet%20Downloads)](https://www.nuget.org/packages/TempStickSharp)

![Logo](https://raw.githubusercontent.com/timheuer/tempstick-sharp/main/src/tempstick-sharp/art/icon.png)

# TempStick#
A simple C# client for the [TempStick](https://www.tempstick.com/) USB temperature sensor API.

## Installation
The TempStick# library is available as a [NuGet package](https://www.nuget.org/packages/TempStickSharp/).
To install TempStickSharp, run the following command in the [Package Manager Console](https://docs.nuget.org/docs/start-here/using-the-package-manager-console)

	PM> Install-Package TempStickSharp

## Usage
The TempStick# library is a simple wrapper around the TempStick API.  It provides a simple interface to get the sensor information and the readings from the sensors.

### Retrieve a list of sensors
```csharp
var client = new TempStickClient("YOUR_API_KEY");
var sensors = await client.GetSensorsAsync();

foreach (var sensor in sensors.Data.Sensors)
{
	Console.WriteLine(sensor.SensorName);
}
```

### Retrieve the readings for a sensor
```csharp
var client = new TempStickClient("YOUR_API_KEY");
var readings = await client.GetReadingsAsync("SENSOR_ID");

foreach (var reading in readings.Data.Readings)
{
	Console.WriteLine(reading.Humidity);
	Console.WriteLine(reading.Temperature);
}
```

### Retrieve readings for a period of time
```csharp
var client = new TempStickClient("YOUR_API_KEY");
var readings = await client.GetReadingsAsync("SENSOR_ID", 32400, "30_days", null, null);

foreach (var reading in readings.Data.Readings)
{
	Console.WriteLine(reading.Humidity);
	Console.WriteLine(reading.Temperature);
}
```
## Support
For any support, please log a bug

## License
TempStick# is licensed under the MIT License

## Icon 
Thermometer by Anggara Putra from [Noun Project](https://thenounproject.com/browse/icons/term/thermometer/) (CC BY 3.0)
