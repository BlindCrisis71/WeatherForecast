using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using WeatherForecast.HttpClientService;

namespace WeatherForecast.Hubs
{
    public class WeatherHub : Hub
    {
        public async Task SendCoordinates(string lat, string lng)
        {
            WeatherService weatherService = new WeatherService();
            double dlat = 0.0;
            double dlng = 0.0;
            dlat = Convert.ToDouble(lat);
            dlng = Convert.ToDouble(lng);

            string result = await weatherService.GetCurrentWeatherAsync(dlng,dlat);
            await Clients.All.SendAsync("ReceiveResult", result);
        }
    }
}