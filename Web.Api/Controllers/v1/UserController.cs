using Application.Commands.User.Create;
using Application.Commands.User.Delete;
using Application.Commands.User.GetById;
using Application.Commands.User.Update;
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

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteUser(Guid id) {
    var deleteUserResult = await _mediator.Send(new DeleteUserCommand(id));

    return deleteUserResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }

  [HttpGet("{id:guid}")]
  [Authorize]
  public async Task<IActionResult> GetUserById(Guid id) {
    var getUserResult = await _mediator.Send(new GetByIdUserCommand(id));

    return getUserResult.Match(userDto => Ok(userDto),
                               errors => Problem(errors));
  }

  [HttpPut]
  public async Task<IActionResult>
  UpdateUser([FromBody] UpdateUserCommand command) {

    var updateUserResult = await _mediator.Send(command);

    return updateUserResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }
}
