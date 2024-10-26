using ErrorOr;
using MediatR;

namespace Application.Commands.Order.Delete;

public record DeleteOrderCommand(Guid OrderId) : IRequest<ErrorOr<Unit>>;
