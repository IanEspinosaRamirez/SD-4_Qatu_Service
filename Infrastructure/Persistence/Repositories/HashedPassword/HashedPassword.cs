using Domain.HashedPassword;

public class HashedPassword : IHashedPassword {
  public string HashPassword(string password) {
    try {
      return BCrypt.Net.BCrypt.HashPassword(password);
    } catch (Exception ex) {
      throw new InvalidOperationException("Error hashing the password.", ex);
    }
  }

  public bool VerifyPassword(string password, string hashedPassword) {
    if (!hashedPassword.StartsWith("$2a$") &&
        !hashedPassword.StartsWith("$2b$") &&
        !hashedPassword.StartsWith("$2y$")) {
      throw new FormatException("Invalid hash format.");
    }

    return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
  }
}
