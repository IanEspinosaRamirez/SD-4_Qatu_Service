using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Web.Api.Extensions;

public static class JwtExtensions {
  public static IServiceCollection
  AddJwtAuthentication(this IServiceCollection services,
                       IConfiguration configuration) {
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => {
          options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
          };

          options.Events =
              new JwtBearerEvents { OnMessageReceived = context => {
                if (context.Request.Cookies.ContainsKey("AuthToken")) {
                  context.Token = context.Request.Cookies["AuthToken"];
                }
                return Task.CompletedTask;
              } };
        });

    return services;
  }
}
