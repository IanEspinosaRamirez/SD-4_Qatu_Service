using Domain.Entities.OrderDetails;
using Domain.Entities.ReviewProducts;
using Domain.Entities.Photos;
using Domain.Primitives;
using Domain.Entities.Coupons;
using Domain.Entities.Products.Enums;
using Domain.Entities.Stores;
using Domain.Entities.Categories;

namespace Domain.Entities.Products;

public class Product : AggregateRoot
{
    public Product() { }

    public Product(CustomerId id, string name, float price, string description,
                   int stock, string brand,
                   CustomerId storeId, CustomerId categoryId)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
        Stock = stock;
        Brand = brand;
        CreatedAt = DateTime.Now;
        UpdatedAt = null;
        Status = StatusProduct.ACTIVE;
        StoreId = storeId;
        CategoryId = categoryId;
        ReviewProducts = new List<ReviewProduct>();
        OrderDetails = new List<OrderDetail>();
        Photos = new List<Photo>();
        Coupons = new List<Coupon>();
    }

    public CustomerId Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public string Description { get; set; }
    public int Stock { get; set; }
    public string Brand { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public StatusProduct Status { get; set; }

    // Claves foráneas
    public CustomerId StoreId { get; set; }
    public CustomerId CategoryId { get; set; }

    // Propiedades de navegación
    public Store Store { get; set; }
    public Category Category { get; set; }

    // Relaciones
    public ICollection<ReviewProduct> ReviewProducts { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
    public ICollection<Photo> Photos { get; set; }
    public ICollection<Coupon> Coupons { get; set; }
}
