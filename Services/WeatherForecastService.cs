using System;
using System.Threading.Tasks;
using webapi_m_sqlserver.Domain.Models;
using webapi_m_sqlserver.Domain.Repositories;
using webapi_m_sqlserver.Domain.Services;
using webapi_m_sqlserver.Domain.Services.Communication;

namespace webapi_m_sqlserver.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IWeatherForecastRepository _weatherForecastRepository;
        private readonly IUnitOfWork _unitOfWork;

        public WeatherForecastService(IWeatherForecastRepository weatherForecastRepository, IUnitOfWork unitOfWork) 
        {
            _weatherForecastRepository = weatherForecastRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericResponse<WeatherForecast>> FindByIdAsync(FindByIdAsyncRequest filter)
        {
            try
            {
                var dbEntity = await _weatherForecastRepository.FindByIdAsync(filter.Id);

                if (dbEntity == null)
                    return new GenericResponse<WeatherForecast>("Weather forecast not found.");

                return new GenericResponse<WeatherForecast>(dbEntity);
            }
            catch (Exception ex)
            {
                return new GenericResponse<WeatherForecast>($"Search failed: {ex.Message}");
            }
        }

        public async Task<ListAsyncResponse<WeatherForecast>> ListAsync(ListAsyncRequest filter)
        {
            return await _weatherForecastRepository.ListAsync(filter);
        }

        public async Task<GenericResponse<WeatherForecast>> SaveAsync(WeatherForecast entity)
        {
            try
            {
                if (entity.Id == 0) 
                {
                    await _weatherForecastRepository.AddAsync(entity);
                } 
                else 
                {
                    var dbEntity = await _weatherForecastRepository.FindByIdAsync(entity.Id);

                    if (dbEntity == null)
                        return new GenericResponse<WeatherForecast>("Weather forecast not found.");

                    // Update properties
                    dbEntity.Date = entity.Date;
	                dbEntity.TemperatureC = entity.TemperatureC;
                    dbEntity.Summary = entity.Summary;

                    _weatherForecastRepository.Update(dbEntity);
                }
                await _unitOfWork.CompleteAsync();
                return new GenericResponse<WeatherForecast>(entity);
            }
            catch (Exception ex)
            {
                return new GenericResponse<WeatherForecast>($"Error saving: {ex.Message}");
            }
        }
    }
}