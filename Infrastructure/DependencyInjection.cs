using Domain.Primitives;
using Application.Data;

using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.HashedPassword;

namespace Infrastructure;

public static class DependencyInjection {
  public static IServiceCollection
  AddInfrastructure(this IServiceCollection services,
                    IConfiguration configuration) {
    services.AddPersistence(configuration);
    return services;
  }

  private static IServiceCollection
  AddPersistence(this IServiceCollection services,
                 IConfiguration configuration) {
    var connectionString =
        configuration.GetConnectionString("DefaultConnection");
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));

    services.AddDbContext<ApplicationDbContext>(
        options => options.UseMySql(connectionString, serverVersion));

    services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

    services.AddScoped<IUnitOfWork, UnitOfWork>();

    services.AddScoped<IHashedPassword, HashedPassword>();

    return services;
  }
}
