using Application.Commands.User.Create;
using Application.Commands.User.Delete;
using Application.Commands.User.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ApiController
{
    private readonly ISender _mediator;

    public UserController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult>
    CreateUser([FromBody] CreateUserCommand command)
    {
        var createUserResult = await _mediator.Send(command);

        return createUserResult.Match(
            _ => StatusCode(201), errors => Problem(errors));
    }

    [HttpDelete]
    public async Task<IActionResult>
    DeleteUser([FromBody] DeleteUserCommand command)
    {
        var deleteUserResult = await _mediator.Send(command);

        return deleteUserResult.Match(
            _ => StatusCode(204), errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var getUserResult = await _mediator.Send(new GetByIdUserCommand(id));

        return getUserResult.Match(userDto => Ok(userDto),
                                   errors => Problem(errors));
    }
}
