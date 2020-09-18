using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using webapi_m_sqlserver.Domain.Models;
using webapi_m_sqlserver.Domain.Services;

namespace webapi_m_sqlserver.Services.Background
{
    public class ExampleListener : RabbitMQListener
    {
        private readonly ILogger<ExampleListener> _logger;

        // Because the HandleMessage function is a delegate callback, if you inject other services directly, they are not in the same scope,
        // To call other Service instances here, you can only get instance objects after IServiceProvider CreateScope
        private readonly IServiceProvider _services;

        public ExampleListener(IOptions<AppSettings> appSettings, ILogger<ExampleListener> logger, IServiceProvider services) : base(appSettings)
        {
            base.RouteKey = "my.topic";
            base.QueueName = "my.example.queue";
            _logger = logger;
            _services = services;
        }

        public override async Task<bool> HandleMessage(string message)
        {
            var taskMessage = JsonConvert.DeserializeObject<WeatherForecast>(message);
            if (taskMessage == null)
            {
                // When false is returned, the message is rejected directly, indicating that it cannot be processed
                return false;
            }
            try
            {
                using (var scope = _services.CreateScope())
                {
                    var scopedProcessingService = scope.ServiceProvider.GetRequiredService<IWeatherForecastService>();
                    var result = await scopedProcessingService.SaveAsync(taskMessage);
                    return result.Success;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"ExampleListener - HandleMessage fail,error:{ex.Message},stackTrace:{ex.StackTrace},message:{message}");
                _logger.LogError(-1, ex, "ExampleListener - HandleMessage fail");
                return false;
            }

        }
    }
}