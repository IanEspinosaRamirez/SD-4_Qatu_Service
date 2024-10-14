using Domain.Primitives;

namespace Domain.Entities.ReviewStores;

public class ReviewStore : AggregateRoot {
  public CustomerId Id { get; set; }

  public int Rating { get; set; }

  public String Content { get; set; }

  public DateTime CreatedAt { get; set; }
}
