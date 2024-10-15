using Domain.Entities.Products;
using Domain.Primitives;

namespace Domain.Entities.Categories;

public class Category : AggregateRoot
{
    public CustomerId Id { get; set; }

    public String Name { get; set; }

    // Relationship with Product (1 Category -> Many Products)
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
