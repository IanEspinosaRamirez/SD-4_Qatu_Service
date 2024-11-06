using Application.Queries.Product.DTOs;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using System.Linq.Expressions;

namespace Application.Queries.Product.GetPaged;

internal sealed class GetProductsPagedQueryHandler
    : IRequestHandler<GetProductsPagedQuery,
                      ErrorOr<List<ResponseGetPagedProductDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsPagedQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<List<ResponseGetPagedProductDto>>>
    Handle(GetProductsPagedQuery query, CancellationToken cancellationToken)
    {
        var orderByExpression =
            GetOrderByExpression<Domain.Entities.Products.Product>(
                query.OrderByField);

        var products = await _unitOfWork.ProductRepository.GetPaged(
            query.PageNumber, query.PageSize, query.FilterField, query.FilterValue,
            orderByExpression, query.Ascending);

        if (products == null || !products.Any())
        {
            return Error.Failure("Product.NoRecords", "No products found.");
        }

        var productDtos = products
                              .Select(product => new ResponseGetPagedProductDto
                              {
                                  Id = product.Id.Value.ToString(),
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

    private Expression<Func<T, object>>? GetOrderByExpression<T>(
        string? orderByField)
    {
        if (string.IsNullOrEmpty(orderByField))
        {
            return null;
        }

        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.PropertyOrField(parameter, orderByField);
        var conversion = Expression.Convert(property, typeof(object));
        return Expression.Lambda<Func<T, object>>(conversion, parameter);
    }
}
