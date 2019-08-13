using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DarkSky.Models;
using DarkSky.Services;
using DarkSky.Extensions;
using WeatherForecast.HttpClientService;
using GoogleMaps.LocationServices;
using WeatherForecast.Hubs;


namespace WeatherForecast {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.Configure<CookiePolicyOptions>(options => {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSignalR();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSignalR(routes =>
            {
                routes.MapHub<WeatherHub>("/weatherHub");
            });

            app.UseMvc();

            // TEST HARD CODED WEATHER-GET LOCATION
            WeatherService weatherService = new WeatherService();
            Console.WriteLine("TESTING WEATHER LOCATION GET FROM - WSU DAVIS");
            //_ = weatherService.GetCurrentWeatherAsync(41.134500, -111.951530);
            _ = GetCurrentWeatherForLocationAsync(41.134500, -111.951530);
        }

        public void TestGeoLocationService()
        {
            var address = "Ogden, UT";
            Console.WriteLine("ADDRESS: " + address);

            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(address);

            var latitude = point.Latitude;
            var longitude = point.Longitude;

            _ = GetCurrentWeatherForLocationAsync(latitude, longitude);
        }

        public async Task GetCurrentWeatherForLocationAsync(double latitude, double longitude)
        {
            WeatherService weatherService = new WeatherService();
            List<string> currentWeatherData = await weatherService.GetCurrentWeatherAsync(latitude, longitude);
            Console.WriteLine("CURRENT WEATHER DATA");
            currentWeatherData.ForEach(Console.WriteLine);
        }
    }
}
