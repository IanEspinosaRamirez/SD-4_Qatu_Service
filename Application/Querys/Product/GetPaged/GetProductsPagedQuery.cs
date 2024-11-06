using Application.Querys.Product.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Querys.Product.GetPaged;

public record
GetProductsPagedQuery(int PageNumber, int PageSize, string? FilterField = null,
                      string? FilterValue = null, string? OrderByField = null,
                      bool Ascending = true)
    : IRequest<ErrorOr<List<ResponseGetPagedProductDto>>>;
