using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace webapi_m_sqlserver.Services.Background
{
    public class RabbitMQListener : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        protected string RouteKey;
        protected string QueueName;

        public RabbitMQListener(IOptions<AppSettings> appSettings)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = appSettings.Value.MQHost,
                    Port = appSettings.Value.MQPort,
                    UserName = appSettings.Value.MQUserName,
                    Password = appSettings.Value.MQPassword,
                    DispatchConsumersAsync = true
                };
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RabbitListener init error,ex:{ex.Message}");
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            Register();
            return Task.CompletedTask;
        }

        public virtual async Task<bool> HandleMessage(string message)
        {
            throw new NotImplementedException();
        }

        // Registered consumer monitoring here
        private void Register()
        {
            _channel.ExchangeDeclare(exchange: "yo.services", type: ExchangeType.Topic);
            _channel.QueueDeclare(queue:QueueName, exclusive: false);
            _channel.QueueBind(queue: QueueName, exchange: "yo.services", routingKey: RouteKey);
            _channel.BasicQos(0, 1, false);
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = Encoding.UTF8.GetString(ea.Body.ToArray());
                var result = await HandleMessage(body);
                // if (result) // This is discussed because it is important that you think about how you will handle messages in the event of an error.
                // {
                    _channel.BasicAck(ea.DeliveryTag, false);
                // }
            };

            _channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}