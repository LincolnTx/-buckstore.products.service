using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using buckstore.products.service.domain.SeedWork;
using buckstore.products.service.domain.Exceptions;
using buckstore.products.service.infrastructure.Data.Context;
using buckstore.products.service.application.IntegrationEvents;
using buckstore.products.service.infrastructure.Data.UnitOfWork;
using buckstore.products.service.infra.environment.Configurations;
using buckstore.products.service.bus.MessageBroker.Kafka.Producers;
using buckstore.products.service.application.Adapters.MessageBroker;
using buckstore.products.service.domain.Aggregates.ProductAggregate;
using buckstore.products.service.infrastructure.Data.Repositories.ProductRepository;

namespace buckstore.products.service.infrastructure.CrossCutting.IoC
{
	public class NativeInjectorBootstrapper
	{
		public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
		{
			RegisterData(services);
			RegisterMediatR(services);
            RegisterProducers(services);
			RegisterEnvironment(services, configuration);
		}

		public static void RegisterData(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IProductRepository, ProductRepository>();
		}

		public static void RegisterMediatR(IServiceCollection services)
		{
			// injection for Mediator
			services.AddScoped<INotificationHandler<ExceptionNotification>, ExceptionNotificationHandler>();
		}

		public static void RegisterEnvironment(IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton(configuration.GetSection("KafkaConfiguration").Get<KafkaConfiguration>());
		}

        public static void RegisterProducers(IServiceCollection services)
        {
            services.AddScoped<IMessageProducer<StockConfirmationIntegrationEvent>, KafkaProducer<StockConfirmationIntegrationEvent>>();
            services.AddScoped<IMessageProducer<StockConfirmationFailIntegrationEvent>, KafkaProducer<StockConfirmationFailIntegrationEvent>>();
            services.AddScoped<IMessageProducer<ProductRollbackIntegrationEvent>, KafkaProducer<ProductRollbackIntegrationEvent>>();
        }
	}
}
