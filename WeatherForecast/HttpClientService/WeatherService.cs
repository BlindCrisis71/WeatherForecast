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

            // ADDING FORECAST WEATHER ELEMENTS - 4 DAYS
            // KEY: [6,9,12,15]Icon, [7,10,13,16]TempH/L, [8,11,14,17]PrecipProbabily

                // CURRENT + 1 
                currentWeatherData.Add(forecast.Response.Daily.Data[1].Icon.ToString());
                var tempHigh = Math.Round((decimal)forecast.Response.Daily.Data[1].TemperatureHigh, MidpointRounding.AwayFromZero).ToString();
                var tempLow = Math.Round((decimal)forecast.Response.Daily.Data[1].TemperatureLow, MidpointRounding.AwayFromZero).ToString();
                currentWeatherData.Add(tempHigh + "/" + tempLow);
                precipProbPercentage = String.Format("{0:P2}", forecast.Response.Daily.Data[1].PrecipProbability).ToString();
                currentWeatherData.Add(precipProbPercentage);

                // CURRENT + 2
                currentWeatherData.Add(forecast.Response.Daily.Data[2].Icon.ToString());
                tempHigh = Math.Round((decimal)forecast.Response.Daily.Data[2].TemperatureHigh, MidpointRounding.AwayFromZero).ToString();
                tempLow = Math.Round((decimal)forecast.Response.Daily.Data[2].TemperatureLow, MidpointRounding.AwayFromZero).ToString();
                currentWeatherData.Add(tempHigh + "/" + tempLow);
                precipProbPercentage = String.Format("{0:P2}", forecast.Response.Daily.Data[2].PrecipProbability).ToString();
                currentWeatherData.Add(precipProbPercentage);

                // CURRENT + 3
                currentWeatherData.Add(forecast.Response.Daily.Data[3].Icon.ToString());
                tempHigh = Math.Round((decimal)forecast.Response.Daily.Data[3].TemperatureHigh, MidpointRounding.AwayFromZero).ToString();
                tempLow = Math.Round((decimal)forecast.Response.Daily.Data[3].TemperatureLow, MidpointRounding.AwayFromZero).ToString();
                currentWeatherData.Add(tempHigh + "/" + tempLow);
                precipProbPercentage = String.Format("{0:P2}", forecast.Response.Daily.Data[3].PrecipProbability).ToString();
                currentWeatherData.Add(precipProbPercentage);

                // CURRENT + 4
                currentWeatherData.Add(forecast.Response.Daily.Data[4].Icon.ToString());
                tempHigh = Math.Round((decimal)forecast.Response.Daily.Data[4].TemperatureHigh, MidpointRounding.AwayFromZero).ToString();
                tempLow = Math.Round((decimal)forecast.Response.Daily.Data[4].TemperatureLow, MidpointRounding.AwayFromZero).ToString();
                currentWeatherData.Add(tempHigh + "/" + tempLow);
                precipProbPercentage = String.Format("{0:P2}", forecast.Response.Daily.Data[4].PrecipProbability).ToString();
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
