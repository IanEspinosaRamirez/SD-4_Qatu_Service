using ErrorOr;
using MediatR;

namespace Application.Commands.Store.Delete;

public record DeleteStoreCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
