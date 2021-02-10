using System;
using buckstore.products.service.infrastructure.Data.Context;
using Microsoft.Extensions.DependencyInjection;

namespace buckstore.products.service.infrastructure.CrossCutting.IoC.Configurations
{
	public static class DatabaseSetup
	{
		public static void AddDatabaseSetup(this IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Scoped);
		}
	}
}