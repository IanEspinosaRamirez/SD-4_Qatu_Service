using Application.Queries.Product.GetById;
using Application.Queries.Prouduct.DTOs;
using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Queries.Prouduct.GetById;

internal sealed class GetByIdProductQueryHandler
    : IRequestHandler<GetByIdProductQuery, ErrorOr<ResponseGetProductByIdDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdProductQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<ResponseGetProductByIdDto>>
    Handle(GetByIdProductQuery query, CancellationToken cancellationToken)
    {
        var product =
            await _unitOfWork.ProductRepository.GetById(new CustomerId(query.Id));

        if (product is null)
        {
            return Error.Failure("Product.NotFound", "Product not found.");
        }

        var productDto = new ResponseGetProductByIdDto
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
            Brand = product.Brand,
        };

        return productDto;
    }
}
