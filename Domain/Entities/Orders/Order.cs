using Domain.Primitives;
using Domain.Entities.Users;
using Domain.Entities.OrderDetails;

namespace Domain.Entities.Orders;

public class Order : AggregateRoot {
  public CustomerId Id { get; set; }

  public float TotalPrice { get; set; }

  public String ShippingMethod { get; set; }

  public String PaymentMethod { get; set; }

  public DateTime OrderDate { get; set; }

  // Foreign Key for User (1 Order -> 1 User)
  public CustomerId UserId { get; set; }

  // Navigation property for User
  public User User { get; set; }

  // Relationship with OrderDetail (1 Order -> Many OrderDetails)
  public ICollection<OrderDetail> OrderDetails {
    get; set;
  } = new List<OrderDetail>();
}
