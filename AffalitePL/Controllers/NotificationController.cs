using AffaliteBL.DTOs.Auth;
using AffaliteBL.DTOs.NotificationDTOs;
using AffaliteBL.IServices;
using AffalitePL.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AffalitePL.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult CreateNotification([FromBody] CreateNotificationDTO model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDTO<object>
            {
                Success = false,
                Message = "Invalid model",
                Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()
            });
        }

        var result = _notificationService.CreateNotification(model);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpGet("")]
    public IActionResult GetUserNotifications([FromQuery] NotificationQueryParams queryParams)
    {
        var userId = User.GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized(new ApiResponseDTO<object>
            {
                Success = false,
                Message = "Unauthorized",
                Errors = new List<string> { "Invalid token subject" }
            });
        }

        var result = _notificationService.GetUserNotifications(userId, queryParams);
        return Ok(result);
    }

    [HttpGet("unread-count")]
    public IActionResult GetUnreadCount()
    {
        var userId = User.GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized(new ApiResponseDTO<object>
            {
                Success = false,
                Message = "Unauthorized",
                Errors = new List<string> { "Invalid token subject" }
            });
        }

        var result = _notificationService.GetUnreadCount(userId);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetNotificationById(int id)
    {
        var userId = User.GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized(new ApiResponseDTO<object>
            {
                Success = false,
                Message = "Unauthorized",
                Errors = new List<string> { "Invalid token subject" }
            });
        }

        var result = _notificationService.GetNotificationById(id, userId);
        if (!result.Success)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [HttpPut("{id}/read")]
    public IActionResult MarkAsRead(int id)
    {
        var userId = User.GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized(new ApiResponseDTO<object>
            {
                Success = false,
                Message = "Unauthorized",
                Errors = new List<string> { "Invalid token subject" }
            });
        }

        var result = _notificationService.MarkAsRead(id, userId);
        if (!result.Success)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [HttpPut("read-all")]
    public IActionResult MarkAllAsRead()
    {
        var userId = User.GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized(new ApiResponseDTO<object>
            {
                Success = false,
                Message = "Unauthorized",
                Errors = new List<string> { "Invalid token subject" }
            });
        }

        var result = _notificationService.MarkAllAsRead(userId);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteNotification(int id)
    {
        var userId = User.GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized(new ApiResponseDTO<object>
            {
                Success = false,
                Message = "Unauthorized",
                Errors = new List<string> { "Invalid token subject" }
            });
        }

        var result = _notificationService.DeleteNotification(id, userId);
        if (!result.Success)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
}