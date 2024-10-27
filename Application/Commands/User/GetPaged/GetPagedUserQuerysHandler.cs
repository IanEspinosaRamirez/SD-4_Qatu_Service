using Application.Commands.User.DTOs;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.User.GetPaged;

internal sealed class GetUsersPagedQueryHandler
    : IRequestHandler<GetUsersPagedQuery,
                      ErrorOr<List<ResponseGetPagedUserDto>>> {

  private readonly IUnitOfWork _unitOfWork;

  public GetUsersPagedQueryHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<List<ResponseGetPagedUserDto>>>
  Handle(GetUsersPagedQuery request, CancellationToken cancellationToken) {
    var users = await _unitOfWork.UserRepository.GetPaged(request.PageNumber,
                                                          request.PageSize);

    if (!users.Any()) {
      return Error.Failure("User.NoRecords", "No users found.");
    }

    var userDtos =
        users
            .Select(user => new ResponseGetPagedUserDto {
              FullName = user.FullName, Email = user.Email, Phone = user.Phone,
              Username = user.Username, Country = user.Country,
              CreatedAt = user.CreatedAt, UpdatedAt = user.UpdatedAt,
              Address = user.Address, ImageURL = user.ImageURL,
              RoleUser = user.RoleUser.ToString(),
              VerifiedAccount = user.VerifiedAccount
            })
            .ToList();

    return userDtos;
  }
}
