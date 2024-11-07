using Application.Commands.Coupon.Create;
using Application.Commands.Coupon.Delete;
using Application.Queries.Coupon.GetById;
using Application.Queries.Coupon.GetPaged;
using Application.Commands.Coupon.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class CouponController : ApiController
{
    private readonly ISender _mediator;

    public CouponController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    [Authorize(Roles = "Administrator, Seller")]
    public async Task<IActionResult>
    CreateCoupon([FromBody] CreateCouponCommand command)
    {
        var createCouponResult = await _mediator.Send(command);

        return createCouponResult.Match(
            _ => StatusCode(201), errors => Problem(errors));
    }

    [HttpPut]
    [Authorize(Roles = "Administrator, Seller")]
    public async Task<IActionResult>
    UpdateCoupon([FromBody] UpdateCouponCommand command)
    {
        var updateCouponResult = await _mediator.Send(command);

        return updateCouponResult.Match(
            _ => StatusCode(204), errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetCoupon(Guid id)
    {
        var getCouponResult = await _mediator.Send(new GetByIdCouponQuery(id));

        return getCouponResult.Match(coupon => Ok(coupon),
                                     errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator, Seller")]
    public async Task<IActionResult> DeleteCoupon(Guid id)
    {
        var deleteCouponResult = await _mediator.Send(new DeleteCouponCommand(id));

        return deleteCouponResult.Match(
            _ => StatusCode(204), errors => Problem(errors));
    }

    [HttpGet("paged")]
    [Authorize(Roles = "Administrator, Seller")]
    public async Task<IActionResult>
    GetCouponsPaged(int pageNumber = 1, int pageSize = 10,
                    string? filterField = null, string? filterValue = null,
                    string? orderByField = null, bool ascending = true)
    {
        var getPagedResult = await _mediator.Send(
            new GetCouponsPagedQuery(pageNumber, pageSize, filterField, filterValue,
                                     orderByField, ascending));

        return getPagedResult.Match(coupons => Ok(coupons),
                                    errors => Problem(errors));
    }
}
