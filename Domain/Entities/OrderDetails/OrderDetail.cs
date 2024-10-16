using Domain.Primitives;
using Domain.Entities.Orders;
using Domain.Entities.Products;

namespace Domain.Entities.OrderDetails;

public class OrderDetail : AggregateRoot {
  public OrderDetail(CustomerId id, int quantity, float unitPrice,
                     CustomerId orderId, CustomerId productId) {
    Id = id;
    Quantity = quantity;
    UnitPrice = unitPrice;
    OrderId = orderId;
    ProductId = productId;
  }

  public CustomerId Id { get; set; }
  public int Quantity { get; set; }
  public float UnitPrice { get; set; }

  // Claves foráneas
  public CustomerId OrderId { get; set; }
  public CustomerId ProductId { get; set; }

  // Propiedades de navegación
  public Order? Order { get; set; }
  public Product? Product { get; set; }
}
