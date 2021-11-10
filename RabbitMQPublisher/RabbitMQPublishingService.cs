using Microsoft.Extensions.Logging;
using MS.RabbitMQCore.Connector;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Tasks;

namespace MS.RabbitMQCore.RabbitMQPublisher
{
    public class RabbitMQPublishingService : IRabbitMQPublishingService
    {
        #region CTOR
        private readonly IRabbitMQConnection _rabbitMQConnection;
        private readonly ILogger<IRabbitMQPublishingService> _logger;

        public RabbitMQPublishingService(IRabbitMQConnection rabbitMQConnection, ILogger<IRabbitMQPublishingService> logger)
        {
            _rabbitMQConnection = rabbitMQConnection;
            _logger = logger;
        }
        #endregion

        public async Task Publish(string queueName, string routingProperties, string basicProeprties, object body)
        {
            if (string.IsNullOrEmpty(queueName))
                queueName = "rabbitmq.default.queue";
            using (IConnection connection = await _rabbitMQConnection.GetConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    channel.BasicPublish(exchange: queueName, routingKey: queueName, basicProperties: null, body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body)));
                }
            }

        }

        public async Task Publish<TModel>(string queueName, string routingProperties, string basicProeprties, TModel body)
        {
            await Publish(queueName, routingProperties, basicProeprties, body);
        }
    }
}
