using Domain.Primitives;

namespace Domain.Entities.Photos;

public class Photo : AggregateRoot {
  public CustomerId Id { get; set; }

  public String ImageURL { get; set; }
}
