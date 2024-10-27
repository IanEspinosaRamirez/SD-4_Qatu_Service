using System.Security.Claims;
using Application.Commands.User.Create;
using Application.Commands.User.Delete;
using Application.Commands.User.GetById;
using Application.Commands.User.GetPaged;
using Application.Commands.User.Update;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    var match = System.Text.RegularExpressions.Regex.Match(userIdClaim,
                                                           @"[0-9a-fA-F-]{36}");
    var userId = Guid.Parse(match.Value);

    var deleteUserResult =
        await _mediator.Send(new DeleteUserCommand(new CustomerId(userId)));

    return deleteUserResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }

  [HttpGet]
  [Authorize]
  public async Task<IActionResult> GetUserById() {
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    var match = System.Text.RegularExpressions.Regex.Match(userIdClaim,
                                                           @"[0-9a-fA-F-]{36}");
    var userId = Guid.Parse(match.Value);

    var getUserResult =
        await _mediator.Send(new GetByIdUserCommand(new CustomerId(userId)));

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

  [Authorize(Roles = "Administrator")]
  [HttpGet("paged")]
  public async Task<IActionResult> GetUsersPaged(int pageNumber = 1,
                                                 int pageSize = 10) {
    var getPagedResult =
        await _mediator.Send(new GetUsersPagedQuery(pageNumber, pageSize));

    return getPagedResult.Match(users => Ok(users), errors => Problem(errors));
  }
}
