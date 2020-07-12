using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_m_sqlserver.Domain.Models
{
    public class WeatherForecast
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public Continents Continent { get; set; }
    }
}
