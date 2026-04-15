using AffaliteBL.DTOs.Auth;
using AffaliteBL.IServices;
using AffaliteDAL.Entities.Constants;
using AffalitePL.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AffalitePL.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.Admin)]
public class AdminDashboardController : ControllerBase
{
    private readonly IAdminDashboardService _dashboardService;

    public AdminDashboardController(IAdminDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("overview")]
    public IActionResult GetOverview()
    {
        var result = _dashboardService.GetDashboardOverview();
        return Ok(result);
    }

    [HttpGet("summary")]
    public IActionResult GetSummary()
    {
        var result = _dashboardService.GetSummary();
        return Ok(result);
    }

    [HttpGet("recent-orders")]
    public IActionResult GetRecentOrders([FromQuery] int count = 10)
    {
        var result = _dashboardService.GetRecentOrders(count);
        return Ok(result);
    }

    [HttpGet("top-products")]
    public IActionResult GetTopProducts([FromQuery] int count = 5)
    {
        var result = _dashboardService.GetTopProducts(count);
        return Ok(result);
    }

    [HttpGet("top-affiliates")]
    public IActionResult GetTopAffiliates([FromQuery] int count = 5)
    {
        var result = _dashboardService.GetTopAffiliates(count);
        return Ok(result);
    }

    [HttpGet("revenue-chart")]
    public IActionResult GetRevenueChart()
    {
        var result = _dashboardService.GetRevenueChart();
        return Ok(result);
    }
}