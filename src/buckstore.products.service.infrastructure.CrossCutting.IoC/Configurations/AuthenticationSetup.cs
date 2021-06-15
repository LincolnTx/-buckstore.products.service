using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace buckstore.products.service.infrastructure.CrossCutting.IoC.Configurations
{
    public static class AuthenticationSetup
    {
        public static void AddAuthenticationSetup(this IServiceCollection services)
        {
            var jwtSettingsSecret = Environment.GetEnvironmentVariable("JwtSettings__Secret");
            var jwtSettingsAudience = Environment.GetEnvironmentVariable("JwtSettings__Audience");
            var jwtSettingsIssuer = Environment.GetEnvironmentVariable("JwtSettings__Issuer");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var key = Encoding.ASCII.GetBytes(jwtSettingsSecret);

                    var tokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidIssuer = jwtSettingsIssuer,
                        ValidAudience = jwtSettingsAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };

                    options.TokenValidationParameters = tokenValidationParameters;
                });
        }
    }
}