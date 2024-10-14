using Domain.Primitives;

namespace Domain.Entities.Stores;

public class Store : AggregateRoot {
  public CustomerId Id { get; set; }

  public String Name { get; set; }

  public String Description { get; set; }

  public String Address { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }
}
