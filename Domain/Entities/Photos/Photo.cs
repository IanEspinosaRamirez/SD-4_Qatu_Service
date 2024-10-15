using Domain.Primitives;

namespace Domain.Entities.Photos;

public class Photo : AggregateRoot {
  public Photo(CustomerId id, string imageURL, CustomerId? productId,
               CustomerId? storeId) {
    Id = id;
    ImageURL = imageURL;
    ProductId = productId; // Optional foreign key
    StoreId = storeId;     // Optional foreign key
  }

  public CustomerId Id { get; set; }

  public string ImageURL { get; set; }

  // Foreign Key for Product (optional)
  public CustomerId? ProductId { get; set; }

  // Foreign Key for Store (optional)
  public CustomerId? StoreId { get; set; }
}
