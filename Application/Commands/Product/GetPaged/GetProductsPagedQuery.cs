using Application.Commands.Product.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Commands.Product.GetPaged;

public record GetProductsPagedQuery(int PageNumber, int PageSize)
    : IRequest<ErrorOr<List<ResponseGetPagedProductDto>>>;
