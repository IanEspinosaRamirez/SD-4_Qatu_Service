using Domain.Entities;
using Domain.HashedPassword;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.User.Create;

internal sealed class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, ErrorOr<Unit>> {
  private readonly IUnitOfWork _unitOfWork;
  private readonly IHashedPassword _passwordHasher;

  public CreateUserCommandHandler(IUnitOfWork unitOfWork,
                                  IHashedPassword passwordHasher) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    _passwordHasher = passwordHasher ??
                      throw new ArgumentNullException(nameof(passwordHasher));
  }

  public async Task<ErrorOr<Unit>> Handle(CreateUserCommand command,
                                          CancellationToken cancellationToken) {
    var hashedPassword = _passwordHasher.HashPassword(command.Password);

    var user = new Domain.Entities.Users.User(
        new CustomerId(Guid.NewGuid()), command.FullName, command.Email,
        command.Username, hashedPassword, command.Country, command.Address,
        command.Phone, command.ImageURL);

    await _unitOfWork.UserRepository.Add(user);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
