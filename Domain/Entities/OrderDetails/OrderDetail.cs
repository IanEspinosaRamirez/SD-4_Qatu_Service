using Domain.Primitives;

namespace Domain.Entities.OrderDetails;

public class OrderDetail : AggregateRoot {
  public CustomerId Id { get; set; }

  public int Quantity { get; set; }

  public float UnitPrice { get; set; }
}
