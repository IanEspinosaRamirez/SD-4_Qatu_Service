using Domain.Entities.CartItems;
using Domain.Entities.Users;
using Domain.Primitives;

namespace Domain.Entities.Carts;

public class Cart : AggregateRoot {
  public Cart(CustomerId id, CustomerId userId) {
    Id = id;
    UserId = userId;
    CartItems = new List<CartItem>();
  }

  public CustomerId Id { get; set; }

  // Clave foránea
  public CustomerId UserId { get; set; }

  // Propiedad de navegación
  public User? User { get; set; }

  // Relaciones
  public ICollection<CartItem> CartItems { get; set; }
}
