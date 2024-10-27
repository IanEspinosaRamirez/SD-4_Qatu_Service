using Application.Commands.Coupon.Create;
using Application.Commands.Coupon.Delete;
using Application.Commands.Coupon.GetById;
using Application.Commands.Coupon.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers.v1;

[Authorize(Roles = "Administrator, Seller")]
[ApiController]
[Route("api/v1/[controller]")]
public class CouponController : ApiController {
  private readonly ISender _mediator;

  public CouponController(ISender mediator) {
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }

  [HttpPost]
  public async Task<IActionResult>
  CreateCoupon([FromBody] CreateCouponCommand command) {
    var createCouponResult = await _mediator.Send(command);

    return createCouponResult.Match(
        _ => StatusCode(201), errors => Problem(errors));
  }

  [HttpPut]
  public async Task<IActionResult>
  UpdateCoupon([FromBody] UpdateCouponCommand command) {
    var updateCouponResult = await _mediator.Send(command);

    return updateCouponResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }

  [HttpGet("{id:guid}")]
  public async Task<IActionResult> GetCoupon(Guid id) {
    var getCouponResult = await _mediator.Send(new GetByIdCouponCommand(id));

    return getCouponResult.Match(coupon => Ok(coupon),
                                 errors => Problem(errors));
  }

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteCoupon(Guid id) {
    var deleteCouponResult = await _mediator.Send(new DeleteCouponCommand(id));

    return deleteCouponResult.Match(
        _ => StatusCode(204), errors => Problem(errors));
  }
}
