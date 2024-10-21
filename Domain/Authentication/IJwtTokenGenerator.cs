using Domain.Entities.Users;

namespace Domain.Authentication;

public interface IJwtTokenGenerator {
  string GenerateToken(User user, bool withoutExpiration);
}
