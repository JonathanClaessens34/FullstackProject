using AppLogic.Events;
using Infrastructure.EventBus;
using Infrastructure.EventBus.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Api
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRabbitMQEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            RabbitMQEventBusOptions options = configuration.GetSection(RabbitMQEventBusOptions.SectionName)
                .Get<RabbitMQEventBusOptions>();

            //Register singleton that manages the event bus subscriptions -> in-memory
            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            //Register the connection with the event bus
            var factory = new ConnectionFactory
            {
                HostName = options.Host,
                DispatchConsumersAsync = true,
                UserName = options.UserName,
                Password = options.Password
            };
            services.AddSingleton<IRabbitMQPersistentConnection>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                return new DefaultRabbitMQPersistentConnection(factory, logger, options.RetryCount);
            });

            //Register the event bus
            services.AddSingleton<IEventBus>(provider =>
            {
                var connection = provider.GetRequiredService<IRabbitMQPersistentConnection>();
                var logger = provider.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var subscriptionsManager = provider.GetRequiredService<IEventBusSubscriptionsManager>();
                string queueName = options.QueueName;
                return new EventBusRabbitMQ(connection, logger, provider, subscriptionsManager, queueName, options.RetryCount);
            });
        }
    }
}