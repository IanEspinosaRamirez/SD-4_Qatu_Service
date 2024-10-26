using Application.Commands.Order.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Commands.Order.GetById;

public record GetByIdOrderCommand(Guid Id) : IRequest<ErrorOr<ResponseGetOrderByIdDto>>;
