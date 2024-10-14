using Domain.Entities.Stores;
using Domain.Entities.Users;
using Domain.Primitives;

namespace Domain.Entities.ReviewStores;

public class ReviewStore : AggregateRoot {
  public CustomerId Id { get; set; }

  public int Rating { get; set; }

  public String Content { get; set; }

  public DateTime CreatedAt { get; set; }

  // Foreign Key for the User relationship
  public CustomerId UserId { get; set; }
  public User User { get; set; }

  // Foreign Key for the Store relationship
  public CustomerId StoreId { get; set; }
  public Store Store { get; set; }
}
