using AutoMapper;
using webapi_m_sqlserver.Domain.Models;
using webapi_m_sqlserver.Resources;

namespace webapi_m_sqlserver.Mapping
{
    public class WeatherForecastSummaryResolver : IValueResolver<WeatherForecast,  WeatherForecastResource, string>
    {
        public string Resolve(WeatherForecast source, WeatherForecastResource destination, string member, ResolutionContext context)
        {
            return source.TemperatureC switch
            {
                var x when x < -5 => "Freezing", 
                var x when x < 0 => "Bracing", 
                var x when x < 5 => "Chilly",
                var x when x < 10 => "Cool",
                var x when x < 15 => "Mild",
                var x when x < 20 => "Warm",
                var x when x < 25 => "Balmy",
                var x when x < 30 => "Hot",
                var x when x < 35 => "Sweltering",
                var x when x >= 35 => "Scorching",
                _ => "Unknown"
            };
        }
    }
}