using Application.Commands.User.DTOs;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.User.GetById;

internal sealed class GetByIdUserCommandHandler
    : IRequestHandler<GetByIdUserCommand, ErrorOr<ResponseGetUserByIdDto>> {
  private readonly IUnitOfWork _unitOfWork;

  public GetByIdUserCommandHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<ResponseGetUserByIdDto>>
  Handle(GetByIdUserCommand command, CancellationToken cancellationToken) {

    var user = await _unitOfWork.UserRepository.GetById(command.Id);

    if (user is null) {
      return Error.Failure("User.NotFound", "User not found.");
    }

    var userDto = new ResponseGetUserByIdDto {
      FullName = user.FullName,   Email = user.Email,
      Phone = user.Phone,         Username = user.Username,
      Country = user.Country,     CreatedAt = user.CreatedAt,
      UpdatedAt = user.UpdatedAt, Address = user.Address,
      ImageURL = user.ImageURL,   UserRole = user.RoleUser.ToString()
    };

    return userDto;
  }
}
