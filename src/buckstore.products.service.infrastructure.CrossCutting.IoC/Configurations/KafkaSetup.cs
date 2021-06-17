using System;
using buckstore.products.service.application.IntegrationEvents;
using buckstore.products.service.infra.environment.Configurations;
using MassTransit;
using MassTransit.KafkaIntegration;
using MassTransit.Registration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace buckstore.products.service.infrastructure.CrossCutting.IoC.Configurations
{
    public static class KafkaSetup
    {
        private static KafkaConfiguration _kafkaConfiguration;

        public static void AddKafka(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            _kafkaConfiguration = configuration.GetSection("KafkaConfiguration").Get<KafkaConfiguration>();

            services.AddMassTransit(bus =>
            {
                bus.UsingInMemory((ctx, cfg) => cfg.ConfigureEndpoints(ctx));

                bus.AddRider(rider =>
                {
                    rider.AddConsumers();
                    rider.AddProducers();
                    
                    rider.UsingKafka((ctx, k) =>
                    {
                        k.Host(_kafkaConfiguration.ConnectionString);
                        
                        //k.TopicEndpoint();
                    });
                });
            });

            services.AddMassTransitHostedService();
        }

        private static void AddConsumers(this IRegistrationConfigurator rider)
        {
            // add consumer
        }

        private static void AddProducers(this IRiderRegistrationConfigurator rider)
        {
            rider.AddProducer<ProductCreatedIntegrationEvent>(_kafkaConfiguration.ProductsToOrders);
            rider.AddProducer<ProductUpdatedIntegrationEvent>(_kafkaConfiguration.ProductsToOrders);
        }
    }
}