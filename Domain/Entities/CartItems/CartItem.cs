using Domain.Entities.Carts;
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

  // Clave foránea
  public CustomerId CartId { get; set; }

  // Propiedad de navegación
  public Cart? Cart { get; set; }
}
