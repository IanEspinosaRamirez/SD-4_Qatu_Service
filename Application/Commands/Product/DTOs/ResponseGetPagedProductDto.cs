using Domain.Entities;

namespace Application.Commands.Product.DTOs;

public class ResponseGetPagedProductDto
{
    public required string Name { get; set; }
    public required float Price { get; set; }
    public required string Description { get; set; }
    public required int Stock { get; set; }
    public required string Brand { get; set; }
    public required CustomerId StoreId { get; set; }
    public required CustomerId CategoryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
