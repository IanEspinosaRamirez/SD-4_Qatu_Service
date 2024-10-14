using Domain.Entities.Carts;
using Domain.Entities.Orders;
using Domain.Entities.ReviewProducts;
using Domain.Entities.ReviewStores;
using Domain.Entities.Stores;
using Domain.Entities.Users.Enums;
using Domain.Primitives;

namespace Domain.Entities.Users;

public class User : AggregateRoot {

  public CustomerId Id { get; set; }

  public String FullName { get; set; }

  public String Email { get; set; }

  public String Phone { get; set; }

  public String Username { get; set; }

  public String Password { get; set; }

  public String Country { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public Boolean ActiveAccount { get; set; }

  public Boolean VerifiedAccount { get; set; }

  public String Address { get; set; }

  public String ImageURL { get; set; }

  public UserRole RoleUser { get; set; }

  // Relationship with ReviewStore (1 User -> Many ReviewStores)
  public ICollection<ReviewStore> ReviewStores {
    get; set;
  } = new List<ReviewStore>();

  // Relationship with ReviewProduct (1 User -> Many ReviewProducts)
  public ICollection<ReviewProduct> ReviewProducts {
    get; set;
  } = new List<ReviewProduct>();

  // Relationship with Store (1 User -> Many Stores)
  public ICollection<Store> Stores { get; set; } = new List<Store>();

  // One-to-one relationship with Cart (1 User -> 1 Cart)
  public Cart Cart { get; set; }

  // Relationship with Order (1 User -> Many Orders)
  public ICollection<Order> Orders { get; set; } = new List<Order>();
}
