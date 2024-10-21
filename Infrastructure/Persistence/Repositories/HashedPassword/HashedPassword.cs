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

    return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
  }
}
