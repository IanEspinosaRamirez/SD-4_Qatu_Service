using Domain.Primitives;

namespace Domain.Entities.Carts;

public class Cart : AggregateRoot {
  public CustomerId Id { get; set; }
}
