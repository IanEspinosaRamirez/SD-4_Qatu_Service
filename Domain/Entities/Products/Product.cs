using Domain.Entities.Categories;
using Domain.Entities.OrderDetails;
using Domain.Entities.ReviewProducts;
using Domain.Entities.Photos;
using Domain.Entities.Stores;
using Domain.Primitives;

namespace Domain.Entities.Products;

public class Product : AggregateRoot {

  public CustomerId Id { get; set; }

  public String Name { get; set; }

  public float Price { get; set; }

  public String Description { get; set; }

  public int Stock { get; set; }

  public String Brand { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public String Status { get; set; }

  // Foreign Key for Store (1 Product -> 1 Store)
  public CustomerId StoreId { get; set; }

  // Navigation property for Store
  public Store Store { get; set; }

  // Foreign Key for Category (1 Product -> 1 Category)
  public CustomerId CategoryId { get; set; }

  // Navigation property for Category
  public Category Category { get; set; }

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
}
