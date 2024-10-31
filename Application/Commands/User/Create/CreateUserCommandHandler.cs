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
    var body = GenerateWelcomeEmailBody(command.FullName, command.Email);

    await _emailService.SendEmail(command.Email, subject, body);

    return Unit.Value;
  }

  private string GenerateWelcomeEmailBody(string fullName, string email) {
    return $@"
        <div style='font-family: Arial, sans-serif; color: #432534; background-color: #efd6ac; padding: 20px; border-radius: 10px;'>
            <div style='background-color: #183a37; padding: 15px; border-radius: 10px 10px 0 0; text-align: center;'>
                <h1 style='color: #efd6ac; margin: 0;'>Welcome to Qatu, {fullName}!</h1>
            </div>
            <div style='padding: 20px;'>
                <p style='font-size: 16px;'>Thank you for creating an account with us. We are thrilled to have you on board and look forward to providing you with the best shopping experience.</p>
                
                <div style='margin-top: 20px; padding: 15px; background-color: #efd6ac; border-radius: 8px;'>
                    <h3 style='color: #04151f;'>Here are some quick links to get started:</h3>
                    <ul style='list-style: none; padding: 0;'>
                        <li><a href='https://qatu.com/explore' style='color: #c44900; text-decoration: none;'>Explore Products</a></li>
                        <li><a href='https://qatu.com/profile' style='color: #c44900; text-decoration: none;'>Complete Your Profile</a></li>
                        <li><a href='https://qatu.com/support' style='color: #c44900; text-decoration: none;'>Get Support</a></li>
                    </ul>
                </div>

                <p style='margin-top: 20px;'>If you have any questions, feel free to <a href='mailto:support@qatu.com' style='color: #c44900;'>contact our support team</a>.</p>
            </div>
            
            <div style='text-align: center; color: #efd6ac; background-color: #04151f; padding: 10px; border-radius: 0 0 10px 10px; font-size: 12px;'>
                <p>This email was sent to {email}. If you did not create an account, please ignore this message.</p>
                <p style='margin: 0;'>© 2024 Qatu. All rights reserved.</p>
            </div>
        </div>";
  }
}
