using Application.Commands.ReviewStore.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Commands.ReviewStore.GetPaged;

public record GetReviewStoresPagedQuery(int PageNumber, int PageSize,
                                        string? FilterField = null,
                                        string? FilterValue = null,
                                        string? OrderByField = null,
                                        bool Ascending = true)
    : IRequest<ErrorOr<List<ResponseGetPagedReviewStoreDto>>>;
