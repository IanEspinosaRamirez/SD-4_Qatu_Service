using ErrorOr;
using MediatR;

namespace Application.Commands.OrderDetail.Delete;

public record DeleteOrderDetailCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
