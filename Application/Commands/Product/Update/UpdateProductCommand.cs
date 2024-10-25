using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Commands.Product.Update;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    float Price,
    int Stock,
    string Brand,
    CustomerId CategoryId,
    CustomerId StoreId
) : IRequest<ErrorOr<Unit>>;
