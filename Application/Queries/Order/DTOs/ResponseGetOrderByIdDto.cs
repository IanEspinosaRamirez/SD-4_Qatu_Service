using Domain.Entities;

namespace Application.Queries.Order.DTOs;

public class ResponseGetOrderByIdDto
{
    public required float TotalPrice { get; set; }
    public required string ShippingMethod { get; set; }
    public required string PaymentMethod { get; set; }
    public required CustomerId UserId { get; set; }
}
