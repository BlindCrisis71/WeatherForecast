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

        public async Task<string> GetCurrentWeatherAsync(double latitude, double longitude)
        {
            var darkSky = new DarkSky.Services.DarkSkyService(key);
            var forecast = await darkSky.GetForecast(latitude, longitude);

            if (forecast?.IsSuccessStatus == true)
            {
                Console.WriteLine("CURRENTLY SUMMARY: " + forecast.Response.Currently.Summary);
                return forecast.Response.Currently.Summary;
            }
            else
            {
                Console.WriteLine("No current weather data");
                return "No current weather data";
            }
        }
    }
}
