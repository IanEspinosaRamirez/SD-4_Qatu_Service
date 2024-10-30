using Application.Commands.Product.DTOs;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Product.GetPaged;

internal sealed class GetProductsPagedQueryHandler
    : IRequestHandler<GetProductsPagedQuery, ErrorOr<List<ResponseGetPagedProductDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsPagedQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<List<ResponseGetPagedProductDto>>> Handle(
        GetProductsPagedQuery request,
        CancellationToken cancellationToken
    )
    {
        var products = await _unitOfWork.ProductRepository.GetPaged(
            request.PageNumber,
            request.PageSize
        );

        if (!products.Any())
        {
            return Error.Failure("Product.NoRecords", "No products found.");
        }

        var productDtos = products
            .Select(product => new ResponseGetPagedProductDto
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Stock = product.Stock,
                Brand = product.Brand,
                StoreId = product.StoreId,
                CategoryId = product.CategoryId,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
            })
            .ToList();

        return productDtos;
    }
}
