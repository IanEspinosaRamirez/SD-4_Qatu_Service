using Domain.Primitives;
using Domain.Entities.Users;
using Domain.Entities.OrderDetails;

namespace Domain.Entities.Orders;

public class Order : AggregateRoot
{
    public Order() { }

    public Order(CustomerId id, float totalPrice, string shippingMethod,
                 string paymentMethod, DateTime orderDate, CustomerId userId)
    {
        Id = id;
        TotalPrice = totalPrice;
        ShippingMethod = shippingMethod;
        PaymentMethod = paymentMethod;
        OrderDate = orderDate;
        UserId = userId;
    }

    public CustomerId Id { get; set; }
    public float TotalPrice { get; set; }
    public string ShippingMethod { get; set; }
    public string PaymentMethod { get; set; }
    public DateTime OrderDate { get; set; }

    // Clave foránea
    public CustomerId UserId { get; set; }

    // Propiedad de navegación
    public User? User { get; set; }

    // Relaciones
    public ICollection<OrderDetail> OrderDetails
    {
        get; set;
    } = new List<OrderDetail>();
}
