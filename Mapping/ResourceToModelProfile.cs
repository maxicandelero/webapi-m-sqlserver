using AutoMapper;
using webapi_m_sqlserver.Domain.Models;
using webapi_m_sqlserver.Resources;

namespace webapi_m_sqlserver.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<WeatherForecastResource, WeatherForecast>();
        }
    }
}