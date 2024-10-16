using Domain.Entities.Products;
using Domain.Entities.Users;
using Domain.Primitives;

namespace Domain.Entities.ReviewProducts;

public class ReviewProduct : AggregateRoot {
  public ReviewProduct(CustomerId id, int rating, string content,
                       CustomerId userId, CustomerId productId) {
    Id = id;
    Rating = rating;
    Content = content;
    CreatedAt = DateTime.Now;
    UserId = userId;
    ProductId = productId;
  }

  public CustomerId Id { get; set; }
  public int Rating { get; set; }
  public string Content { get; set; }
  public DateTime CreatedAt { get; set; }

  // Claves foráneas
  public CustomerId UserId { get; set; }
  public CustomerId ProductId { get; set; }

  // Propiedades de navegación
  public User? User { get; set; }
  public Product? Product { get; set; }
}
