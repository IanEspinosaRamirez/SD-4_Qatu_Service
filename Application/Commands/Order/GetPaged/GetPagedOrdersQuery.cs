using Application.Commands.Order.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Commands.Order.GetPaged;

public record GetPagedOrdersQuery(int PageNumber, int PageSize)
    : IRequest<ErrorOr<List<ResponseGetPagedOrderDto>>>;
