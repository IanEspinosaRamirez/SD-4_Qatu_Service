using Domain.Primitives;

namespace Domain.Entities.ReviewProducts;

public class ReviewProduct : AggregateRoot {
  public ReviewProduct(CustomerId id, int rating, string content,
                       CustomerId userId, CustomerId productId) {
    Id = id;
    Rating = rating;
    Content = content;
    CreatedAt = DateTime.Now;
    UserId = userId;       // Only the UserId is passed
    ProductId = productId; // Only the ProductId is passed
  }

  public CustomerId Id { get; set; }

  public int Rating { get; set; }

  public String Content { get; set; }

  public DateTime CreatedAt { get; set; }

  // Foreign Key for the User relationship
  public CustomerId UserId { get; set; }

  // Foreign Key for the Product relationship
  public CustomerId ProductId { get; set; }
}
