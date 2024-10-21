using Domain.Authentication;
using Domain.HashedPassword;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.User.Login;

internal sealed class LoginUserCommandHandler
    : IRequestHandler<LoginUserCommand, ErrorOr<string>> {
  private readonly IUnitOfWork _unitOfWork;
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IHashedPassword _passwordHasher;

  public LoginUserCommandHandler(IUnitOfWork unitOfWork,
                                 IJwtTokenGenerator jwtTokenGenerator,
                                 IHashedPassword passwordHasher) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(
                                                  nameof(jwtTokenGenerator));
    _passwordHasher = passwordHasher ??
                      throw new ArgumentNullException(nameof(passwordHasher));
  }

  public async Task<ErrorOr<string>>
  Handle(LoginUserCommand command, CancellationToken cancellationToken) {

    var user = await _unitOfWork.UserRepository.GetUserByLoginIdentifierAsync(
        command.LoginIdentifier);

    if (user == null) {
      return Error.Failure("Authentication.InvalidCredentials",
                           "Invalid login credentials.");
    }

    Console.WriteLine(user);
    var passwordMatch =
        _passwordHasher.VerifyPassword(command.Password, user.Password);

    if (!passwordMatch) {
      return Error.Failure("Authentication.InvalidCredentials",
                           "Invalid login credentials.");
    }

    var token = _jwtTokenGenerator.GenerateToken(user, command.Remember);

    return token;
  }
}
