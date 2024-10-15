using Domain.Entities.Coupons;
using Domain.Entities.Products;
using Domain.Primitives;

namespace Domain.Entities.Categories;

public class Category : AggregateRoot {
  public Category(CustomerId id, string name,
                  ICollection<Product>? products = null,
                  ICollection<Coupon>? coupons = null) {
    Id = id;
    Name = name;
    Products = products ??
               new List<Product>(); // Initialize as empty list if not provided
    Coupons = coupons ??
              new List<Coupon>(); // Initialize as empty list if not provided
  }

  public CustomerId Id { get; set; }

  public String Name { get; set; }

  // Relationship with Product (1 Category -> Many Products)
  public ICollection<Product> Products { get; set; } = new List<Product>();

  // Relationship with Coupon (1 Category -> Many Coupons)
  public ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
}
