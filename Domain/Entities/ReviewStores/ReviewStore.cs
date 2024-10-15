using Domain.Primitives;

namespace Domain.Entities.ReviewStores;

public class ReviewStore : AggregateRoot {
  public ReviewStore(CustomerId id, int rating, string content,
                     CustomerId userId, CustomerId storeId) {
    Id = id;
    Rating = rating;
    Content = content;
    CreatedAt = DateTime.Now;
    UserId = userId;   // Only the UserId is passed
    StoreId = storeId; // Only the StoreId is passed
  }

  public CustomerId Id { get; set; }

  public int Rating { get; set; }

  public String Content { get; set; }

  public DateTime CreatedAt { get; set; }

  // Foreign Key for the User relationship
  public CustomerId UserId { get; set; }

  // Foreign Key for the Store relationship
  public CustomerId StoreId { get; set; }
}
