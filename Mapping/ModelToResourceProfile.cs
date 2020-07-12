using AutoMapper;
using webapi_m_sqlserver.Domain.Models;
using webapi_m_sqlserver.Domain.Services.Communication;
using webapi_m_sqlserver.Resources;

namespace webapi_m_sqlserver.Mapping
{
    public class ModelToResourceProfile: Profile
    {
        public ModelToResourceProfile()
        {
            // WeatherForecast
            CreateMap<WeatherForecast, WeatherForecastResource>()
                .ForMember(c => c.Summary, opt => opt.MapFrom<WeatherForecastSummaryResolver>());
            CreateMap<ListAsyncResponse<WeatherForecast>, ListAsyncResponse<WeatherForecastResource>>();           
        }
    }
}