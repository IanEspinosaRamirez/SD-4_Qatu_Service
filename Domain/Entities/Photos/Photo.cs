using Domain.Primitives;
using Domain.Entities.Products;
using Domain.Entities.Stores;

namespace Domain.Entities.Photos;

public class Photo : AggregateRoot {
  public CustomerId Id { get; set; }

  public String ImageURL { get; set; }

  // Foreign Key for Product (optional)
  public CustomerId? ProductId { get; set; }

  // Navigation property for Product
  public Product Product { get; set; }

  // Foreign Key for Store (optional)
  public CustomerId? StoreId { get; set; }

  // Navigation property for Store
  public Store Store { get; set; }
}
