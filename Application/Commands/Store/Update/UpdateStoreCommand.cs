using ErrorOr;
using MediatR;

namespace Application.Commands.Store.Update;

public record UpdateStoreCommand(Guid Id, string Name,
                                 string Description,
                                 string Address, Guid UserId) : IRequest<ErrorOr<Unit>>;



