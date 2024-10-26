using Domain.Entities;

namespace Application.Commands.Order.DTOs;

public class ResponseGetOrderByIdDto
{
    public required float TotalPrice { get; set; }
    public required string ShippingMethod { get; set; }
    public required string PaymentMethod { get; set; }
    public required CustomerId UserId { get; set; }
}
