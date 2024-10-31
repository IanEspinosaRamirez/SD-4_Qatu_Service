using Domain.Email;
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
  private readonly IEmailService _emailService;

  public CreateUserCommandHandler(IUnitOfWork unitOfWork,
                                  IHashedPassword passwordHasher,
                                  IEmailService emailService) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    _passwordHasher = passwordHasher ??
                      throw new ArgumentNullException(nameof(passwordHasher));
    _emailService =
        emailService ?? throw new ArgumentNullException(nameof(emailService));
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

    var subject = "Welcome to Qatu!";
    var body =
        $"<p>Hi {command.FullName},</p><p>Your account has been successfully created on Qatu. " +
        "We are excited to have you onboard!</p>";

    await _emailService.SendEmail(command.Email, subject, body);

    return Unit.Value;
  }
}
