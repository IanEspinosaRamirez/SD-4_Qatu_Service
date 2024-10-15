using Domain.Primitives;

namespace Domain.Entities.CartItems;

public class CartItem : AggregateRoot {
  public CartItem(CustomerId id, int quantity, CustomerId cartId) {
    Id = id;
    Quantity = quantity;
    CartId = cartId;
  }

  public CustomerId Id { get; set; }

  public int Quantity { get; set; }

  // Foreign Key for Cart (1 CartItem -> 1 Cart)
  public CustomerId CartId { get; set; }
}
