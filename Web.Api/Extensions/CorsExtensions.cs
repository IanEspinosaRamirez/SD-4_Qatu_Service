
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Api.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("SpecificOriginPolicy", builder =>
            {
                builder.WithOrigins(configuration["Cors:AllowedOrigin"])
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });

        return services;
    }
}