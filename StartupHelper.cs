using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MS.RabbitMQCore.Connector;
using MS.RabbitMQCore.Models;
using MS.RabbitMQCore.RabbitMQPublisher;
using System;

namespace MS.RabbitMQCore
{
    public static class StartupHelper
    {
        /// <summary>
        /// Rabbit MQ Service Add using Morpheus Soft Library Support
        /// </summary>
        /// <param name="services"></param>
        /// <param name="host"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="port"></param>
        public static void AttachMSRabbitMQ(this IServiceCollection services, string host, string username, string password, int port = 5672)
        {
            services.PostConfigure<RabbitMQConfiguration>(c =>
            {
                c = new RabbitMQConfiguration();
            });
            services.AddCommonServices();
        }
        /// <summary>
        /// Configuration Json AppSettings : "{configurationName}": {"Host": "server1.ashiqemran.com","Username": "MSMassTransitUser","Password": "MSTransitPassword","Port": 5472}
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        /// <param name="configurationName"></param>
        public static void AttachMSRabbitMQ(this IServiceCollection services, IConfiguration Configuration, string configurationName)
        {
            services.PostConfigure<RabbitMQConfiguration>(options =>
            {
                foreach (var item in typeof(RabbitMQConfiguration).GetProperties())
                    item.SetValue(options, Convert.ChangeType(Configuration[configurationName + ":" + item.Name], item.PropertyType));
            });
            services.AddCommonServices();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="rabbitMQConfiguration"></param>
        public static void AttachMSRabbitMQ(this IServiceCollection services, RabbitMQConfiguration rabbitMQConfiguration)
        {
            services.PostConfigure<RabbitMQConfiguration>(options =>
            {
                options = rabbitMQConfiguration;
            });
            services.AddCommonServices();
        }
        internal static void AddCommonServices(this IServiceCollection services)
        {
            services.AddSingleton<IRabbitMQConnection, RabbitMQConnection>();
            services.AddSingleton<IRabbitMQPublishingService, RabbitMQPublishingService>();
        }
    }
}
