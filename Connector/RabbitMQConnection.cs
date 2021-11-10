using Microsoft.Extensions.Options;
using MS.RabbitMQCore.Models;
using RabbitMQ.Client;
using System.Threading.Tasks;

namespace MS.RabbitMQCore.Connector
{
    public class RabbitMQConnection : IRabbitMQConnection
    {
        #region CTOR
        internal readonly string _rabbitMQUser;
        internal readonly string _rabbitMQPassword;
        internal readonly string _rabbitMQHost;
        internal readonly int _rabbitMQPort;
        private IConnection _connection;
        public RabbitMQConnection(IOptions<RabbitMQConfiguration> rabbitMQConfiguration)
        {

            _rabbitMQUser = rabbitMQConfiguration.Value.Username;
            _rabbitMQPassword = rabbitMQConfiguration.Value.Password;
            _rabbitMQHost = rabbitMQConfiguration.Value.Host;
            _rabbitMQPort = rabbitMQConfiguration.Value.Port;
        }
        #endregion

        public Task<IConnection> GetConnection()
        {
            _connection = _connection ?? GetInitConnection();
            return Task.FromResult(_connection);
        }

        public Task<IConnection> GetConnection(string username, string password, string host, int port = 5672)
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                UserName = username,
                Password = password,
                HostName = host,
                Port = port
            };
            IConnection conn = factory.CreateConnection();
            return Task.FromResult(conn);
        }

        private IConnection GetInitConnection()
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                UserName = _rabbitMQUser,
                Password = _rabbitMQPassword,
                HostName = _rabbitMQHost,
                Port = _rabbitMQPort
            };
            return factory.CreateConnection();
        }
    }
}
