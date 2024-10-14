using Domain.Entities;
using Domain.Primitives;

public class Product : AggregateRoot {

  public CustomerId Id { get; set; }

  public String Name { get; set; }

  public float Price { get; set; }

  public String Description { get; set; }

  public int Stock { get; set; }

  public String Brand { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public String Status { get; set; }
}
