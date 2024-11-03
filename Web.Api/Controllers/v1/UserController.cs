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
public class UserController : ApiController
{
    private readonly ISender _mediator;

    public UserController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var createUserResult = await _mediator.Send(command);

        return createUserResult.Match(_ => StatusCode(201), errors => Problem(errors));
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteUser()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Console.WriteLine(userIdClaim);

        var deleteUserResult = await _mediator.Send(
            new DeleteUserCommand(new CustomerId(Guid.Parse(userIdClaim!)))
        );

        return deleteUserResult.Match(_ => StatusCode(204), errors => Problem(errors));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserById()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        Console.WriteLine(userIdClaim);

        if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var userId))
        {
            return BadRequest("Invalid user ID");
        }

        var getUserResult = await _mediator.Send(new GetByIdUserCommand(new CustomerId(userId)));

        return getUserResult.Match(userDto => Ok(userDto), errors => Problem(errors));
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
    {
        var updateUserResult = await _mediator.Send(command);

        return updateUserResult.Match(_ => StatusCode(204), errors => Problem(errors));
    }

    [HttpGet("paged")]
    [Authorize(Roles = "Administrator, Seller")]
    public async Task<IActionResult> GetUsersPaged(
        int pageNumber = 1,
        int pageSize = 10,
        string? filterField = null,
        string? filterValue = null,
        string? orderByField = null,
        bool ascending = true
    )
    {
        var getPagedResult = await _mediator.Send(
            new GetUsersPagedQuery(
                pageNumber,
                pageSize,
                filterField,
                filterValue,
                orderByField,
                ascending
            )
        );

        return getPagedResult.Match(users => Ok(users), errors => Problem(errors));
    }
}
