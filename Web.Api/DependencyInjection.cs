using Web.Api.Extensions;
using Web.Api.Middlewares;
using Google.Cloud.Storage.V1;

namespace Web.Api;

public static class DependencyInjection {
  public static IServiceCollection
  AddPresentation(this IServiceCollection services,
                  IConfiguration configuration) {
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddTransient<HandleErrors>();

    services.AddSingleton(StorageClient.Create());

    services.AddCustomCors(configuration);
    services.AddJwtAuthentication(configuration);

    return services;
  }
}
