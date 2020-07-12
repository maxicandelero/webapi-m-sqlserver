using System;
using System.ComponentModel.DataAnnotations;
using webapi_m_sqlserver.Domain.Models;

namespace webapi_m_sqlserver.Resources
{
    public class WeatherForecastResource
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "TemperatureC is required.")]
        public int TemperatureC { get; set; }
        [Range((int)Continents.Africa, (int)Continents.SouthAmerica, ErrorMessage = "Continent is required.")]
        public Continents Continent { get; set; }
        public string Summary { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}