using System;
using GreenPipes;
using MassTransit;
using MassTransit.Registration;
using MassTransit.KafkaIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using buckstore.products.service.application.IntegrationEvents;
using buckstore.products.service.infra.environment.Configurations;
using buckstore.products.service.bus.MessageBroker.Kafka.Consumers;

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

                        k.TopicEndpoint<OrderReceivedIntegrationEvent>(_kafkaConfiguration.ProductsFromOrders, _kafkaConfiguration.Group,
                            e =>
                        {
                            e.ConfigureConsumer<OrderReceivedConsumer>(ctx);
                            e.CreateIfMissing(options =>
                            {
                                options.NumPartitions = 3;
                                options.ReplicationFactor = 1;
                            });
                        });

                        k.TopicEndpoint<ProductCreatedIntegrationEvent>(_kafkaConfiguration.ProductsFromManagerCreate, _kafkaConfiguration.ConnectionString,
                            e =>
                        {
                            e.UseMessageRetry(retryCfg =>
                            {
                                retryCfg.Handle<RetryLimitExceededException>();
                                retryCfg.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(10),
                                    TimeSpan.FromMinutes(20));
                                retryCfg.Immediate(5);
                            });
                            e.ConfigureConsumer<CreatedProductConsumer>(ctx);
                            e.CreateIfMissing(options =>
                            {
                                options.NumPartitions = 3;
                                options.ReplicationFactor = 1;
                            });
                        });

                        k.TopicEndpoint<ProductUpdatedIntegrationEvent>(_kafkaConfiguration.ProductsFromManagerUpdate, _kafkaConfiguration.ConnectionString,
                            e =>
                        {
                            e.UseMessageRetry(retryCfg =>
                            {
                                retryCfg.Handle<RetryLimitExceededException>();
                                retryCfg.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(10),
                                    TimeSpan.FromMinutes(20));
                                retryCfg.Immediate(5);
                            });
                            e.ConfigureConsumer<UpdatedProductConsumer>(ctx);
                            e.CreateIfMissing(options =>
                            {
                                options.NumPartitions = 3;
                                options.ReplicationFactor = 1;
                            });
                        });

                        k.TopicEndpoint<ProductDeletedIntegrationEvent>(_kafkaConfiguration.ProductsFromManagerDelete, _kafkaConfiguration.ConnectionString,
                            e =>
                        {
                            e.UseMessageRetry(retryCfg =>
                            {
                                retryCfg.Handle<RetryLimitExceededException>();
                                retryCfg.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(10),
                                    TimeSpan.FromMinutes(20));
                                retryCfg.Immediate(5);
                            });
                            e.ConfigureConsumer<DeletedProductConsumer>(ctx);
                            e.CreateIfMissing(options =>
                            {
                                options.NumPartitions = 3;
                                options.ReplicationFactor = 1;
                            });
                        });
                    });
                });
            });

            services.AddMassTransitHostedService();
        }

        private static void AddConsumers(this IRegistrationConfigurator rider)
        {
            rider.AddConsumer<OrderReceivedConsumer>();
            rider.AddConsumer<CreatedProductConsumer>();
            rider.AddConsumer<UpdatedProductConsumer>();
            rider.AddConsumer<DeletedProductConsumer>();
        }

        private static void AddProducers(this IRiderRegistrationConfigurator rider)
        {
           rider.AddProducer<StockConfirmationIntegrationEvent>(_kafkaConfiguration.ProductsStockResponseSuccess);
           rider.AddProducer<StockConfirmationFailIntegrationEvent>(_kafkaConfiguration.ProductsStockResponseFail);
        }
    }
}
