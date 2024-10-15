using Domain.Entities.CartItems;
using Domain.Entities.Users;
using Domain.Primitives;

namespace Domain.Entities.Carts;

public class Cart : AggregateRoot {
  public CustomerId Id { get; set; }

  // Foreign Key for the User (1 Cart -> 1 User)
  public CustomerId UserId { get; set; }

  // Navigation property for User
  public User User { get; set; }

  // One-to-many relationship with CartItems (1 Cart -> Many CartItems)
  public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
