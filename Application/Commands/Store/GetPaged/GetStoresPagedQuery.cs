using Application.Commands.Store.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Commands.Store.GetPaged;

public record GetStoresPagedQuery(int PageNumber, int PageSize)
    : IRequest<ErrorOr<List<ResponseGetPagedStoreDto>>>;
