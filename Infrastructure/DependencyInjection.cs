using Domain.Primitives;
using Application.Data;

using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.HashedPassword;
using Domain.Authentication;
using Infrastructure.Persistence.Repositories.Authentication;
using Domain.Email;
using Infrastructure.Services;

namespace Infrastructure;

public static class DependencyInjection {
  public static IServiceCollection
  AddInfrastructure(this IServiceCollection services,
                    IConfiguration configuration) {
    services.AddPersistence(configuration);

    // Agregar configuración de EmailService
    services.Configure<EmailSettings>(
        configuration.GetSection("EmailSettings"));
    services.AddScoped<IEmailService, EmailService>();

    return services;
  }

  private static IServiceCollection
  AddPersistence(this IServiceCollection services,
                 IConfiguration configuration) {
    var connectionString =
        configuration.GetConnectionString("DefaultConnection");
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));

    services.AddSingleton<IJwtTokenGenerator>(new JwtTokenGenerator(
        configuration["Jwt:Key"], configuration["Jwt:Audience"],
        configuration["JwtIssuer"]));

    services.AddDbContext<ApplicationDbContext>(
        options => options.UseMySql(connectionString, serverVersion));

    services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<IHashedPassword, HashedPassword>();

    return services;
  }
}
