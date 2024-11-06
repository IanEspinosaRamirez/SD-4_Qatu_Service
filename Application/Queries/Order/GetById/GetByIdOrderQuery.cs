using Application.Queries.Order.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Queries.Order.GetById;

public record GetByIdOrderQuery(Guid Id)
    : IRequest<ErrorOr<ResponseGetOrderByIdDto>>;
