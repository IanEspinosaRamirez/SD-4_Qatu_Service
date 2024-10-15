using Domain.Entities.OrderDetails;
using Domain.Entities.ReviewProducts;
using Domain.Entities.Photos;
using Domain.Primitives;
using Domain.Entities.Coupons;
using Domain.Entities.Products.Enums;

namespace Domain.Entities.Products;

public class Product : AggregateRoot {
  public Product(CustomerId id, string name, float price, string description,
                 int stock, string brand, DateTime updatedAt,
                 CustomerId storeId, CustomerId categoryId) {
    Id = id;
    Name = name;
    Price = price;
    Description = description;
    Stock = stock;
    Brand = brand;
    CreatedAt = DateTime.Now;
    UpdatedAt = updatedAt;
    Status = StatusProduct.ACTIVE;
    StoreId = storeId;
    CategoryId = categoryId;
    ReviewProducts = new List<ReviewProduct>(); // Initialize as empty list
    OrderDetails = new List<OrderDetail>();     // Initialize as empty list
    Photos = new List<Photo>();                 // Initialize as empty list
    Coupons = new List<Coupon>();               // Initialize as empty list
  }

  public CustomerId Id { get; set; }

  public string Name { get; set; }

  public float Price { get; set; }

  public string Description { get; set; }

  public int Stock { get; set; }

  public string Brand { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public StatusProduct Status { get; set; }

  // Foreign Key for Store (1 Product -> 1 Store)
  public CustomerId StoreId { get; set; }

  // Foreign Key for Category (1 Product -> 1 Category)
  public CustomerId CategoryId { get; set; }

  // Relationship with ReviewProduct (1 Product -> Many ReviewProducts)
  public ICollection<ReviewProduct> ReviewProducts {
    get; set;
  } = new List<ReviewProduct>();

  // Relationship with OrderDetail (1 Product -> Many OrderDetails)
  public ICollection<OrderDetail> OrderDetails {
    get; set;
  } = new List<OrderDetail>();

  // Relationship with Photo (1 Product -> Many Photos)
  public ICollection<Photo> Photos { get; set; } = new List<Photo>();

  // Relationship with Coupon (1 Product -> Many Coupons)
  public ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
}
