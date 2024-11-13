using System.Security.Claims;
using Application.Commands.User.Create;
using Application.Commands.User.Delete;
using Application.Queries.User.GetById;
using Application.Queries.User.GetPaged;
using Application.Commands.User.Update;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Commands.User.UpdateRole;
using Domain.Entities.Users.Enums;
using Application.Commands.User.UpdatePhoto.Dto;
using Application.Commands.User.UpdatePhoto;

namespace Web.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ApiController {
  private readonly ISender _mediator;

  public UserController(ISender mediator) {
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }

  [HttpPost]
  public async Task<IActionResult>
  CreateUser([FromBody] CreateUserCommand command) {
    var createUserResult = await _mediator.Send(command);

    return createUserResult.Match(
        _ => StatusCode(201), errors => Problem(errors));
  }

  [HttpDelete]
  [Authorize]
  public async Task<IActionResult> DeleteUser() {
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    var deleteUserResult = await _mediator.Send(
        new DeleteUserCommand(new CustomerId(Guid.Parse(userIdClaim!))));

    return deleteUserResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }

  [HttpGet]
  [Authorize]
  public async Task<IActionResult> GetUserById() {
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    var getUserResult = await _mediator.Send(
        new GetByIdUserQuery(new CustomerId(Guid.Parse(userIdClaim!))));

    return getUserResult.Match(userDto => Ok(userDto),
                               errors => Problem(errors));
  }

  [HttpPut]
  [Authorize]
  public async Task<IActionResult>
  UpdateUser([FromBody] UpdateUserCommand command) {
    var updateUserResult = await _mediator.Send(command);

    return updateUserResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }

  [HttpGet("paged")]
  public async Task<IActionResult>
  GetUsersPaged(int pageNumber = 1, int pageSize = 10,
                string? filterField = null, string? filterValue = null,
                string? orderByField = null, bool ascending = true) {
    var getPagedResult = await _mediator.Send(
        new GetUsersPagedQuery(pageNumber, pageSize, filterField, filterValue,
                               orderByField, ascending));

    return getPagedResult.Match(users => Ok(users), errors => Problem(errors));
  }

  [HttpPatch("role")]
  [Authorize(Roles = "Administrator")]
  public async Task<IActionResult>
  UpdateUserRole([FromBody] UpdateRoleUserCommand command) {

    var updateRoleResult = await _mediator.Send(command);

    return updateRoleResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }

  [HttpPatch("photo")]
  [Authorize]
  public async Task<IActionResult>
  UpdateUserPhoto([FromBody] RequestUpdatePhotoUserDto dto) {
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    var updatePhotoCommand =
        new UpdatePhotoUserCommand(userIdClaim!, dto.LocalFilePath);
    var updatePhotoResult = await _mediator.Send(updatePhotoCommand);

    return updatePhotoResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }
}
