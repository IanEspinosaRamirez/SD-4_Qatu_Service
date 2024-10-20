using Domain.Entities.Users;
using Domain.Entities.Stores;
using Domain.Primitives;

namespace Domain.Entities.ReviewStores;

public class ReviewStore : AggregateRoot
{
    public ReviewStore() { }

    public ReviewStore(CustomerId id, int rating, string content,
                       CustomerId userId, CustomerId storeId)
    {
        Id = id;
        Rating = rating;
        Content = content;
        CreatedAt = DateTime.Now;
        UserId = userId;
        StoreId = storeId;
    }

    public CustomerId Id { get; set; }
    public int Rating { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }

    // Claves foráneas
    public CustomerId UserId { get; set; }
    public CustomerId StoreId { get; set; }

    // Propiedades de navegación
    public User? User { get; set; }
    public Store? Store { get; set; }
}
