using AffaliteBL.DTOs.CouponDTOs;
using AffaliteBL.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AffalitePL.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CouponController : ControllerBase
{
    private readonly ICouponService _couponService;
    public CouponController(ICouponService couponService)
    {
        _couponService = couponService;
    }

    [HttpPost("apply")]
    public IActionResult ApplyCoupon([FromBody] ApplyCouponDTO dto)
    {
        var result = _couponService.ApplyCoupon(dto.Code, dto.OrderTotal);
        if (!result.IsValid)
            return BadRequest(result);
        return Ok(result);
    }
}
