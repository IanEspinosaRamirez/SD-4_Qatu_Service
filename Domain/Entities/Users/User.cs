using Domain.Entities.Carts;
using Domain.Entities.Orders;
using Domain.Entities.ReviewProducts;
using Domain.Entities.ReviewStores;
using Domain.Entities.Stores;
using Domain.Entities.Users.Enums;
using Domain.Primitives;

namespace Domain.Entities.Users;

public class User : AggregateRoot {
  public User() {}

  public User(CustomerId id, string fullName, string email, string username,
              string password, string country, string address,
              string? phone = null, string? imageURL = null) {

    Id = id;
    FullName = fullName;
    Email = email;
    Phone = phone;
    Username = username;
    Password = password;
    Country = country;
    CreatedAt = DateTime.Now;
    UpdatedAt = null;
    ActiveAccount = true;
    VerifiedAccount = false;
    Address = address;
    ImageURL = imageURL;
    RoleUser = UserRole.Client;
    ReviewStores = new List<ReviewStore>();
    ReviewProducts = new List<ReviewProduct>();
    Stores = new List<Store>();
    Cart = null;
    Orders = new List<Order>();
  }

  public CustomerId Id { get; set; }
  public string FullName { get; set; }
  public string Email { get; set; }
  public string? Phone { get; set; }
  public string Username { get; set; }
  public string Password { get; set; }
  public string Country { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
  public bool ActiveAccount { get; set; }
  public bool VerifiedAccount { get; set; }
  public string Address { get; set; }
  public string? ImageURL { get; set; }
  public UserRole RoleUser { get; set; }

  // Relaciones
  public ICollection<ReviewStore> ReviewStores { get; set; }
  public ICollection<ReviewProduct> ReviewProducts { get; set; }
  public ICollection<Store> Stores { get; set; }
  public Cart? Cart { get; set; } // Puede ser null
  public ICollection<Order> Orders { get; set; }
}
