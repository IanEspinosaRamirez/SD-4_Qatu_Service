using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Authentication;
using Domain.Entities.Users;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Persistence.Repositories.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator {
  private readonly string _key;

  public JwtTokenGenerator(string key) { _key = key; }

  public string GenerateToken(User user, bool withoutExpiration = false) {
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_key);

    var tokenDescriptor = new SecurityTokenDescriptor {
      Subject = new ClaimsIdentity(
          new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                  new Claim(ClaimTypes.Name, user.Username),
                  new Claim(ClaimTypes.Email, user.Email),
                  new Claim(ClaimTypes.Role, user.RoleUser.ToString()) }),
      SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };

    if (!withoutExpiration) {
      tokenDescriptor.Expires = DateTime.UtcNow.AddHours(24);
    }

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }
}