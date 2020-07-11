using System.Linq;
using System.Threading.Tasks;
using webapi_m_sqlserver.Domain.Models;
using webapi_m_sqlserver.Domain.Repositories;
using webapi_m_sqlserver.Domain.Services.Communication;

namespace webapi_m_sqlserver.Persistence.Repositories
{
    public class WeatherForecastRepository : BaseRepository, IWeatherForecastRepository
    {
        public WeatherForecastRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(WeatherForecast entity)
        {
            await _context.WeatherForecasts.AddAsync(entity);
        }
 
        public async Task<WeatherForecast> FindByIdAsync(long id)
        {
            return await _context.WeatherForecasts.FindAsync(id);
        }

        public async Task<ListAsyncResponse<WeatherForecast>> ListAsync(ListAsyncRequest filter)
        {
            IQueryable<WeatherForecast> query = _context.WeatherForecasts;
            return await query.GetPaged(filter.Page, filter.PageSize, filter.SortModels);
        }

        public void Update(WeatherForecast entity)
        {
            _context.WeatherForecasts.Update(entity);
        }
    }
}