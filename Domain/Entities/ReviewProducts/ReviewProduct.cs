using Domain.Entities.Products;
using Domain.Entities.Users;
using Domain.Primitives;

namespace Domain.Entities.ReviewProducts;

public class ReviewProduct : AggregateRoot {
  public CustomerId Id { get; set; }

  public int Rating { get; set; }

  public String Content { get; set; }

  public DateTime CreatedAt { get; set; }

  // Foreign Key for the User relationship
  public CustomerId UserId { get; set; }
  public User User { get; set; }

  // Foreign Key for the Product relationship
  public CustomerId ProductId { get; set; }
  public Product Product { get; set; }
}
