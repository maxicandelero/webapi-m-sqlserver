using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webapi_m_sqlserver.Domain.Models;
using webapi_m_sqlserver.Domain.Services;
using webapi_m_sqlserver.Domain.Services.Communication;
using webapi_m_sqlserver.Resources;

namespace webapi_m_sqlserver.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;
        private readonly IMapper _mapper;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService, IMapper mapper)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ListAsyncResponse<WeatherForecastResource>> ListAsync([FromQuery]ListAsyncRequest filter)
        {
            var result = await _weatherForecastService.ListAsync(filter);
            var resources = _mapper.Map<ListAsyncResponse<WeatherForecast>, ListAsyncResponse<WeatherForecastResource>>(result);
            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]WeatherForecastResource resource)
        {
            var weatherForecast = _mapper.Map<WeatherForecastResource, WeatherForecast>(resource);
            var result = await _weatherForecastService.SaveAsync(weatherForecast);

	        if (!result.Success)
		        return BadRequest(result.Message);

	        var resources = _mapper.Map<WeatherForecast, WeatherForecastResource>(result.Entity);
	        return Ok(resources);
        }

        [HttpGet]
        [Route("FindByIdAsync")]
        public async Task<IActionResult> FindByIdAsync([FromQuery]FindByIdAsyncRequest filter)
        {
            var result = await _weatherForecastService.FindByIdAsync(filter);

            if (!result.Success)
		        return BadRequest(result.Message);

            var resources = _mapper.Map<WeatherForecast, WeatherForecastResource>(result.Entity);
            return Ok(resources);
        }
    }
}
