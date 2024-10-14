using Domain.Primitives;

namespace Domain.Entities.CartItems;

public class CardItem : AggregateRoot {
  public CustomerId Id { get; set; }

  public int Quantity { get; set; }
}
