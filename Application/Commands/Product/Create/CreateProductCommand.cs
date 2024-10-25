using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Commands.Product.Create;

public record CreateProductCommand(
    string Name,
    float Price,
    string Description,
    int Stock,
    string Brand,
    CustomerId storeId,
    CustomerId categoryId
) : IRequest<ErrorOr<Unit>>;
