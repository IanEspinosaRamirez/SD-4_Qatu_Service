using Application.Commands.OrderDetail.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Commands.OrderDetail.GetPaged;

public record GetPagedOrderDetailsQuery(int PageNumber, int PageSize)
    : IRequest<ErrorOr<List<ResponseGetPagedOrderDetailDto>>>;
