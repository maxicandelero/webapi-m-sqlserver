using webapi_m_sqlserver.Domain.Models;
using webapi_m_sqlserver.Domain.Services.Communication;

namespace webapi_m_sqlserver.Domain.Repositories
{
    public interface IWeatherForecastRepository : IGenericRepository<WeatherForecast, ListAsyncRequest>
    {
        // The interface's own methods
    }
}