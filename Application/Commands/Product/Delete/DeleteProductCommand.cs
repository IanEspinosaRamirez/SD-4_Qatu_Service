using ErrorOr;
using MediatR;

namespace Application.Commands.Product.Delete;

public record DeleteProductCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
