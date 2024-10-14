using Domain.Primitives;
using Domain.Entities.Orders;
using Domain.Entities.Products;

namespace Domain.Entities.OrderDetails;

public class OrderDetail : AggregateRoot {
  public CustomerId Id { get; set; }

  public int Quantity { get; set; }

  public float UnitPrice { get; set; }

  // Foreign Key for Order (1 OrderDetail -> 1 Order)
  public CustomerId OrderId { get; set; }

  // Navigation property for Order
  public Order Order { get; set; }

  // Foreign Key for Product (1 OrderDetail -> 1 Product)
  public CustomerId ProductId { get; set; }

  // Navigation property for Product
  public Product Product { get; set; }
}
