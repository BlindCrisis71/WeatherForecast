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
                Console.WriteLine("CURRENT SUMMARY: " + forecast.Response.Currently.Summary);
                currentWeatherData.Add(forecast.Response.Currently.Summary);
                let temp = forecast.Response.Currently.Temperature.ToString;
                currentWeatherData.Add(temp);


                return currentWeatherData;
            }
            else
            {
                Console.WriteLine("No current weather data");
                currentWeatherData.Add("No current weather data");
                return currentWeatherData;
            }
        }
    }
}
