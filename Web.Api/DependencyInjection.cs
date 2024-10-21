
using Web.Api.Middlewares;

namespace Web.Api;

public static class DependencyInjection {
  public static IServiceCollection
  AddPresentation(this IServiceCollection services,
                  IConfiguration configuration) {
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddTransient<HandleErrors>();

    services.AddCors(options => {
      options.AddPolicy("SpecificOriginPolicy", builder => {
        builder.WithOrigins("Cors:AllowedOrigin")
            .AllowAnyHeader()
            .AllowAnyMethod();
      });
    });

    return services;
  }
}
