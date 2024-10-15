using Domain.Entities.Carts;
using Domain.Entities.Orders;
using Domain.Entities.ReviewProducts;
using Domain.Entities.ReviewStores;
using Domain.Entities.Stores;
using Domain.Entities.Users.Enums;
using Domain.Primitives;

namespace Domain.Entities.Users;

public class User : AggregateRoot {
  public User(CustomerId id, string fullName, string email, string username,
              string password, string country, DateTime updatedAt,
              bool verifiedAccount, string address, string? phone = null,
              string? imageURL = null) {
    Id = id;
    FullName = fullName;
    Email = email;
    Phone = phone; // This can be null
    Username = username;
    Password = password;
    Country = country;
    CreatedAt = DateTime.Now;
    UpdatedAt = updatedAt;
    ActiveAccount = true;
    VerifiedAccount = verifiedAccount;
    Address = address;
    ImageURL = imageURL; // This can be null
    RoleUser = UserRole.Client;
    ReviewStores = new List<ReviewStore>();     // Initialize as an empty list
    ReviewProducts = new List<ReviewProduct>(); // Initialize as an empty list
    Stores = new List<Store>();                 // Initialize as an empty list
    Cart = null;                                // Can be null
    Orders = new List<Order>();                 // Initialize as an empty list
  }

  public CustomerId Id { get; set; }

  public String FullName { get; set; }

  public String Email { get; set; }

  public String? Phone { get; set; } // Nullable

  public String Username { get; set; }

  public String Password { get; set; }

  public String Country { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public Boolean ActiveAccount { get; set; }

  public Boolean VerifiedAccount { get; set; }

  public String Address { get; set; }

  public String? ImageURL { get; set; } // Nullable

  public UserRole RoleUser { get; set; }

  // Relationship with ReviewStore (1 User -> Many ReviewStores)
  public ICollection<ReviewStore> ReviewStores {
    get; set;
  } // Initialized to an empty list

  // Relationship with ReviewProduct (1 User -> Many ReviewProducts)
  public ICollection<ReviewProduct> ReviewProducts {
    get; set;
  } // Initialized to an empty list

  // Relationship with Store (1 User -> Many Stores)
  public ICollection<Store> Stores { get; set; } // Initialized to an empty list

  // One-to-one relationship with Cart (1 User -> 1 Cart)
  public Cart? Cart { get; set; } // Can be null

  // Relationship with Order (1 User -> Many Orders)
  public ICollection<Order> Orders { get; set; } // Initialized to an empty list
}
