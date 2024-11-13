using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthenticationController : ApiController {
  private readonly ISender _mediator;

  public AuthenticationController(ISender mediator) {
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginUserCommand command) {
    var loginResult = await _mediator.Send(command);

    return loginResult.Match(token => {
      var cookieOptions = new CookieOptions { HttpOnly = true, Secure = true,
                                              SameSite = SameSiteMode.Strict };

      Response.Cookies.Append("AuthToken", token, cookieOptions);

      return Ok(new { message = "Login successful" });
    }, errors => Problem(errors));
  }

  [HttpPost("logout")]
  [Authorize]
  public IActionResult Logout() {
    Response.Cookies.Delete("AuthToken");
    return Ok(new { message = "Logout successful" });
  }

  [HttpGet("verify-token")]
  [Authorize]
  public IActionResult VerifyToken() { return Ok(true); }
}
