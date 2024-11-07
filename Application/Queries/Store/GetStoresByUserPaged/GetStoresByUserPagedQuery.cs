using Application.Queries.Store.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Queries.Store.GetStoresByUserPaged;

public record
GetStoresByUserPagedQuery(string UserId, int PageNumber, int PageSize,
                          string? FilterField = null,
                          string? FilterValue = null,
                          string? OrderByField = null, bool Ascending = true)
    : IRequest<ErrorOr<List<ResponseGetPagedStoreDto>>>;
