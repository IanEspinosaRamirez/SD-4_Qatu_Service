using Domain.Entities.Products;
using Domain.Entities.ReviewStores;
using Domain.Entities.Photos;
using Domain.Primitives;
using Domain.Entities.Coupons;
using Domain.Entities.Users;

namespace Domain.Entities.Stores;

public class Store : AggregateRoot
{
    public Store() { }

    public Store(CustomerId id, string name, string description, string address,
                 DateTime createdAt, DateTime updatedAt, CustomerId userId)
    {
        Id = id;
        Name = name;
        Description = description;
        Address = address;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        UserId = userId;
    }

    public CustomerId Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Clave foránea explícita
    public CustomerId UserId { get; set; }

    // Relación de navegación
    public User? User { get; set; }

    // Otras relaciones
    public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    public ICollection<ReviewStore> ReviewStores
    {
        get; set;
    } = new List<ReviewStore>();
    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
}
