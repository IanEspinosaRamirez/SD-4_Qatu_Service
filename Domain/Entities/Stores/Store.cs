using Domain.Entities.Products;
using Domain.Entities.ReviewStores;
using Domain.Entities.Photos;
using Domain.Primitives;
using Domain.Entities.Coupons;

namespace Domain.Entities.Stores;

public class Store : AggregateRoot {
  public Store(CustomerId id, string name, string description, string address,
               DateTime createdAt, DateTime updatedAt,
               ICollection<Photo> photos, CustomerId userId,
               ICollection<Product> products,
               ICollection<ReviewStore>? reviewStores = null,
               ICollection<Coupon>? coupons = null) {
    Id = id;
    Name = name;
    Description = description;
    Address = address;
    CreatedAt = createdAt;
    UpdatedAt = updatedAt;
    Photos = photos; // Required
    UserId = userId; // Only UserId is required, not the full User object
    Products = products;
    ReviewStores = reviewStores ?? new List<ReviewStore>(); // Optional
    Coupons = coupons ?? new List<Coupon>();                // Optional
  }

  public CustomerId Id { get; set; }

  public String Name { get; set; }

  public String Description { get; set; }

  public String Address { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  // Required relationship with Photo (1 Store -> Many Photos)
  public ICollection<Photo> Photos { get; set; } = new List<Photo>();

  // Relationship with ReviewStore (1 Store -> Many ReviewStores) - Optional
  public ICollection<ReviewStore>? ReviewStores { get; set; }

  // Foreign Key for User (required)
  public CustomerId UserId { get; set; }

  // Relationship with Product (1 Store -> Many Products)
  public ICollection<Product> Products { get; set; } = new List<Product>();

  // Optional relationship with Coupon (1 Store -> Many Coupons)
  public ICollection<Coupon>? Coupons { get; set; }
}
