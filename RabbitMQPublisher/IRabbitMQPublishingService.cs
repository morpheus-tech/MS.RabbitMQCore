using System.Threading.Tasks;

namespace MS.RabbitMQCore.RabbitMQPublisher
{
    public interface IRabbitMQPublishingService
    {
        /// <summary>
        /// Object based RabbitMQ Publish
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="routingProperties"></param>
        /// <param name="basicProeprties"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task Publish(string exchange, string routingProperties, string basicProeprties, object body);
        /// <summary>
        /// Generic RabbitMQ Publish
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="exchange"></param>
        /// <param name="routingProperties"></param>
        /// <param name="basicProeprties"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task Publish<TModel>(string exchange, string routingProperties, string basicProeprties, TModel body);
    }
}
