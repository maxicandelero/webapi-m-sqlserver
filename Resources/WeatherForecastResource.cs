using System;

namespace webapi_m_sqlserver.Resources
{
    public class WeatherForecastResource
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public string SummaryName { get; set; }
        public int TemperatureF { get; set; }
    }
}