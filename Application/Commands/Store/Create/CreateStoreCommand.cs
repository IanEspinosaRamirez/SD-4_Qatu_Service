using ErrorOr;
using MediatR;

namespace Application.Commands.Store.Create;

public record CreateStoreCommand(string Name, string Description,
                                 string Address, string UserId)
    : IRequest<ErrorOr<Unit>>;
