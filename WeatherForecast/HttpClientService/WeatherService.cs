using System;
using DarkSky.Models;
using DarkSky.Services;
using DarkSky.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherForecast.HttpClientService
{
    public class WeatherService
    {
        private static String key = "2f93e74db440e73d669a55510abc5aeb";
        public WeatherService() { }

        public async Task<List<String>> GetCurrentWeatherAsync(double latitude, double longitude)
        {
            List<String> currentWeatherData = new List<String>();

            var darkSky = new DarkSky.Services.DarkSkyService(key);
            var forecast = await darkSky.GetForecast(latitude, longitude);

            if (forecast?.IsSuccessStatus == true)
            {
                // DEBUG - CONSOLE OUTPUT TESTS
                Console.WriteLine("FORECAST SUCCESS");
                Console.WriteLine("SUMMARY:" + forecast.Response.Currently.Summary);
                Console.WriteLine("ICON:" + forecast.Response.Currently.Icon.ToString());
                Console.WriteLine("TEMPERATURE:" + forecast.Response.Currently.Temperature.ToString());
                Console.WriteLine("TEMP HIGH:" + forecast.Response.Currently.TemperatureHigh.ToString());
                Console.WriteLine("TEMP LOW:" + forecast.Response.Currently.TemperatureLow.ToString());
                Console.WriteLine("PRECIP PROBABILITY:" + forecast.Response.Currently.TemperatureLow.ToString());

                // ADDING WEATHER ELEMENTS TO LIST ARRAY - KEY: [0]Summary, [1]Icon, [2]Temperature, [3]Temp High, [4]Temp Low, [5]PrecipProbability
                currentWeatherData.Add(forecast.Response.Currently.Summary);
                currentWeatherData.Add(forecast.Response.Currently.Icon.ToString());
                var currentTemp = Math.Round((decimal)forecast.Response.Currently.Temperature, MidpointRounding.AwayFromZero).ToString();
                currentWeatherData.Add(currentTemp);
                var currentHigh = Math.Round((decimal)forecast.Response.Daily.Data[0].TemperatureHigh, MidpointRounding.AwayFromZero).ToString();
                currentWeatherData.Add(currentHigh);
                var currentLow = Math.Round((decimal)forecast.Response.Daily.Data[0].TemperatureLow, MidpointRounding.AwayFromZero).ToString();
                currentWeatherData.Add(currentLow);
                var precipProbPercentage = String.Format("{0:P2}", forecast.Response.Currently.PrecipProbability).ToString();
                currentWeatherData.Add(precipProbPercentage);

                return currentWeatherData;
            }
            else
            {
                currentWeatherData.Add("No current weather data");

                return currentWeatherData;
            }
        }
    }
}
