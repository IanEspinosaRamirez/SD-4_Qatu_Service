using Domain.Primitives;
using Domain.Entities.Orders;
using Domain.Entities.Products;

namespace Domain.Entities.OrderDetails;

public class OrderDetail : AggregateRoot {
  public OrderDetail(CustomerId id, int quantity, float unitPrice,
                     CustomerId orderId, Order order, CustomerId productId,
                     Product product) {
    Id = id;
    Quantity = quantity;
    UnitPrice = unitPrice;
    OrderId = orderId;
    Order = order;
    ProductId = productId;
    Product = product;
  }

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
