using System;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using webapi_m_sqlserver.Domain.Services.Background;

namespace webapi_m_sqlserver.Services.Background
{
    public class RabbitMQClient : IMQClient
    {
        private readonly IModel _channel;
        private readonly ILogger _logger;

        public RabbitMQClient(IOptions<AppSettings> appSettings, ILogger<RabbitMQClient> logger)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = appSettings.Value.MQHost,
                    Port = appSettings.Value.MQPort,
                    UserName = appSettings.Value.MQUserName,
                    Password = appSettings.Value.MQPassword,

                };
                var connection = factory.CreateConnection();
                _channel = connection.CreateModel();
            }
            catch (Exception ex)
            {
                logger.LogError(-1, ex, "RabbitMQClient init fail");
            }
            _logger = logger;
        }

        public virtual void PushMessage(string topic, object message)
        {
            _channel.ExchangeDeclare(exchange: "my.exchange", type: ExchangeType.Topic);
            var msgJson = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(msgJson);
            _channel.BasicPublish(
                exchange: "my.exchange",
                routingKey: topic,
                basicProperties: null,
                body: body
            );
        }
    }
}
