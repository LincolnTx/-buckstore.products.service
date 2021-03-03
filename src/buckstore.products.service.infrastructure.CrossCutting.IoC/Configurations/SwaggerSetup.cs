using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace buckstore.products.service.infrastructure.CrossCutting.IoC.Configurations
{
	public static class SwaggerSetup
	{
		public static void AddSwaggerSetup(this IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));
			services.AddSwaggerGen(s =>
			{
				s.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "BuckStore Products API",
					Description = "Api responsável pela interações entre cliente e produtos da loja BuckStore",
					Contact = new OpenApiContact { Name = "Lincoln Teixeira", Email = "lincolnsf98@gmail.com" }
				});
			});
		}
		
		public static void UseSwaggerSetup(this IApplicationBuilder app)
		{
			if (app == null) throw new ArgumentNullException(nameof(app));
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "BuckStore Products Api");
			});
		}
	}
}