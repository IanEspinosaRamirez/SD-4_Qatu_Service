using Domain.Entities.CartItems;
using Domain.Primitives;

namespace Domain.Entities.Carts;

public class Cart : AggregateRoot {
  public Cart(CustomerId id, CustomerId userId,
              ICollection<CartItem>? cartItems = null) {
    Id = id;
    UserId = userId;
    CartItems =
        cartItems ??
        new List<CartItem>(); // Initialize as an empty list if not provided
  }

  public CustomerId Id { get; set; }

  // Foreign Key for the User (1 Cart -> 1 User)
  public CustomerId UserId { get; set; }

  // One-to-many relationship with CartItems (1 Cart -> Many CartItems)
  public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
