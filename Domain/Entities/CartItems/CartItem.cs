using Domain.Entities.Carts;
using Domain.Primitives;

namespace Domain.Entities.CartItems;

public class CartItem : AggregateRoot {
  public CustomerId Id { get; set; }

  public int Quantity { get; set; }

  // Foreign Key for Cart (1 CartItem -> 1 Cart)
  public CustomerId CartId { get; set; }

  // Navigation property for Cart
  public Cart Cart { get; set; }
}
