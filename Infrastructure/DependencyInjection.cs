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

    var jwtSettings = configuration.GetSection("Jwt");
    var jwtKey = jwtSettings.GetValue<string>("Key");

    if (string.IsNullOrEmpty(jwtKey)) {
      throw new InvalidOperationException(
          "JWT Key is not configured properly.");
    }

    var key = configuration["Jwt:Key"];
    var audience = configuration["Jwt:Audience"];
    var issuer = configuration["Jwt:Issuer"];

    services.AddSingleton<IJwtTokenGenerator>(
        new JwtTokenGenerator(key, audience, issuer));

    services.AddDbContext<ApplicationDbContext>(
        options => options.UseMySql(connectionString, serverVersion));

    services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<IHashedPassword, HashedPassword>();

    return services;
  }
}
