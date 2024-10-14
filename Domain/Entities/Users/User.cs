using Domain.Primitives;

namespace Domain.Entities.Users;

public class User : AggregateRoot {

  public CustomerId Id { get; set; }

  public String FullName { get; set; }

  public String Email { get; set; }

  public String Phone { get; set; }

  public String Username { get; set; }

  public String Password { get; set; }

  public String Country { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public Boolean ActiveAccount { get; set; }

  public Boolean VerifiedAccount { get; set; }

  public String Address { get; set; }

  public String ImageURL { get; set; }

  public UserRole RoleUser { get; set; }
}
