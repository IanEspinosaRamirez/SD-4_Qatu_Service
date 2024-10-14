using Domain.Primitives;

namespace Domain.Entities.Orders;

public class Order : AggregateRoot {
  public CustomerId Id { get; set; }

  public float TotalPrice { get; set; }

  public String ShippingMethod { get; set; }

  public String PaymentMethod { get; set; }

  public DateTime OrderDate { get; set; }
}
