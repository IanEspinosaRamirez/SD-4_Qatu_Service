using ErrorOr;
using MediatR;

public record LoginUserCommand(string LoginIdentifier, string Password,
                               bool Remember)
    : IRequest<ErrorOr<string>>;
