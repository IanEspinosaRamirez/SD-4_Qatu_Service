using Domain.Entities.Coupons;
using Domain.Entities.Products;
using Domain.Primitives;

namespace Domain.Entities.Categories;

public class Category : AggregateRoot
{
    public Category() { }

    public Category(CustomerId id, string name)
    {
        Id = id;
        Name = name;
        Products = new List<Product>();
        Coupons = new List<Coupon>();
    }

    public CustomerId Id { get; set; }
    public string Name { get; set; }

    // Relaciones
    public ICollection<Product> Products { get; set; }
    public ICollection<Coupon> Coupons { get; set; }
}
