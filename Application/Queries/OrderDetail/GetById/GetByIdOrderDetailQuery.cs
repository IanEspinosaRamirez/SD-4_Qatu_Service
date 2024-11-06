using Application.Queries.OrderDetail.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Queries.OrderDetail.GetById;

public record GetByIdOrderDetailQuery(Guid Id)
    : IRequest<ErrorOr<ResponseGetByIdOrderDetailDto>>;
