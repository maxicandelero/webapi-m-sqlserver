using webapi_m_sqlserver.Domain.Models;
using webapi_m_sqlserver.Domain.Services.Communication;

namespace webapi_m_sqlserver.Domain.Services
{
    public interface IWeatherForecastService : IGenericService<WeatherForecast, ListAsyncRequest, FindByIdAsyncRequest>
    {
        // The interface's own methods
    }
}