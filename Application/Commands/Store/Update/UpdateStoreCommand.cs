using ErrorOr;
using MediatR;

namespace Application.Commands.Store.Update;

public record UpdateStoreCommand(Guid Id, string Name, string Description,
                                 string Country, string Address, string? Phone = null)
    : IRequest<ErrorOr<Unit>>;
