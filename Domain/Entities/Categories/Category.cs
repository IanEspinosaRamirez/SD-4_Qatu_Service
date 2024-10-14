using Domain.Primitives;

namespace Domain.Entities.Categories;

public class Category : AggregateRoot {
  public CustomerId Id { get; set; }

  public String Name { get; set; }
}
