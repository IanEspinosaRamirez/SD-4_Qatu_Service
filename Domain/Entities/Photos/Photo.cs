using Domain.Entities.Products;
using Domain.Entities.Stores;
using Domain.Primitives;

namespace Domain.Entities.Photos;

public class Photo : AggregateRoot {
  public Photo(CustomerId id, string imageURL, CustomerId? productId = null,
               CustomerId? storeId = null) {
    Id = id;
    ImageURL = imageURL;
    ProductId = productId;
    StoreId = storeId;
  }

  public CustomerId Id { get; set; }
  public string ImageURL { get; set; }

  // Claves foráneas opcionales
  public CustomerId? ProductId { get; set; }
  public CustomerId? StoreId { get; set; }

  // Propiedades de navegación
  public Product? Product { get; set; }
  public Store? Store { get; set; }
}
