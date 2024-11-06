using Application.Queries.ReviewStore.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Queries.ReviewStore.GetPaged;

public record GetReviewStoresPagedQuery(int PageNumber, int PageSize,
                                        string? FilterField = null,
                                        string? FilterValue = null,
                                        string? OrderByField = null,
                                        bool Ascending = true)
    : IRequest<ErrorOr<List<ResponseGetPagedReviewStoreDto>>>;
