using Application.Querys.ReviewProduct.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Querys.ReviewProduct.GetPaged;

public record GetReviewProductsPagedQuery(int PageNumber, int PageSize,
                                          string? FilterField = null,
                                          string? FilterValue = null,
                                          string? OrderByField = null,
                                          bool Ascending = true)
    : IRequest<ErrorOr<List<ResponseGetPagedReviewProductDto>>>;
