using RabbitMQ.Client;
using System.Threading.Tasks;

namespace MS.RabbitMQCore.Connector
{
    public interface IRabbitMQConnection
    {
        /// <summary>
        /// Get RabbitMQ connection 
        /// </summary>
        /// <returns></returns>
        Task<IConnection> GetConnection();
        /// <summary>
        /// Get RabbitMQ Connection
        /// </summary>
        /// <param name="username">RabbitMQ User</param>
        /// <param name="password">RabbitMQ Password</param>
        /// <param name="host">RabbitMQ Host</param>
        /// <param name="port"></param>
        /// <returns></returns>
        Task<IConnection> GetConnection(string username, string password, string host, int port = 5672);
    }
}
