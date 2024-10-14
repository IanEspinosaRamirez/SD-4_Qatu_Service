using Domain.Entities.Products;
using Domain.Entities.ReviewStores;
using Domain.Entities.Photos;
using Domain.Entities.Users;
using Domain.Primitives;

namespace Domain.Entities.Stores;

public class Store : AggregateRoot {
  public CustomerId Id { get; set; }

  public String Name { get; set; }

  public String Description { get; set; }

  public String Address { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  // Relationship with ReviewStore (1 Store -> Many ReviewStores)
  public ICollection<ReviewStore> ReviewStores {
    get; set;
  } = new List<ReviewStore>();

  // Foreign Key for User (optional, nullable)
  public CustomerId? UserId { get; set; }

  // Navigation property for User (Store owner)
  public User User { get; set; }

  // Relationship with Product (1 Store -> Many Products)
  public ICollection<Product> Products { get; set; } = new List<Product>();

  // Relationship with Photo (1 Store -> Many Photos)
  public ICollection<Photo> Photos { get; set; } = new List<Photo>();
}
