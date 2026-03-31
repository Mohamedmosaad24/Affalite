using AffaliteBL.DTOs.Auth;
using AffaliteBL.IServices;
using AffaliteDAL.Entities;
using AffaliteDAL.Entities.Constants;
using AffalitePL.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AffalitePL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthServices authService, UserManager<AppUser> userManager)
    : ControllerBase
{
    [HttpPost("register-affiliate")]
    public async Task<IActionResult> RegisterAffiliate([FromBody] RegisterDTO model)
    {
        var result = await authService.RegisterAffiliateAsync(model);

        if (!result.IsAuthenticated)
        {
            return BadRequest(new ApiResponseDTO<AuthResponseDTO>
            {
                Success = false,
                Message = "Affiliate registration failed",
                Errors = new List<string> { result.Message ?? "Validation error" }
            });
        }

        return Ok(new ApiResponseDTO<AuthResponseDTO>
        {
            Success = true,
            Message = "Affiliate registered successfully",
            Data = result
        });
    }

    [HttpPost("register-merchant")]
    public async Task<IActionResult> RegisterMerchant([FromBody] RegisterDTO model)
    {
        var result = await authService.RegisterMerchantAsync(model);
        if (!result.IsAuthenticated)
        {
            return BadRequest(new ApiResponseDTO<AuthResponseDTO>
            {
                Success = false,
                Message = "Merchant registration failed",
                Errors = new List<string> { result.Message ?? "Validation error" }
            });
        }

        return Ok(new ApiResponseDTO<AuthResponseDTO>
        {
            Success = true,
            Message = "Merchant registered successfully",
            Data = result
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO model)
    {
        var result = await authService.LoginAsync(model);
        if (!result.IsAuthenticated)
        {
            return BadRequest(new ApiResponseDTO<AuthResponseDTO>
            {
                Success = false,
                Message = "Login failed",
                Errors = new List<string> { result.Message ?? "Invalid credentials" }
            });
        }

        return Ok(new ApiResponseDTO<AuthResponseDTO>
        {
            Success = true,
            Message = "Logged in successfully",
            Data = result
        });
    }

    [HttpGet("roles")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await authService.GetRolesAsync();

        return Ok(new ApiResponseDTO<List<RoleReadDTO>>
        {
            Success = true,
            Message = "Roles retrieved successfully",
            Data = roles
        });
    }


    [HttpPost("add-to-role")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> AddRole([FromBody] AddRoleDTO model)
    {
        var result = await authService.AddRoleAsync(model);
        if (!result.Succeeded)
        {
            return BadRequest(new ApiResponseDTO<object>
            {
                Success = false,
                Message = "Add role failed",
                Errors = new List<string> { result.Message }
            });
        }

        return Ok(new ApiResponseDTO<AddRoleDTO>
        {
            Success = true,
            Message = result.Message,
            Data = model
        });
    }

    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
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

        var result = await authService.ChangePasswordAsync(userId, model);
        if (!result.Succeeded)
        {
            return BadRequest(new ApiResponseDTO<object>
            {
                Success = false,
                Message = "Change password failed",
                Errors = new List<string> { result.Message }
            });
        }

        return Ok(new ApiResponseDTO<object>
        {
            Success = true,
            Message = result.Message
        });
    }


    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO model)
    {
        var result = await authService.RefreshTokenAsync(model.Token);

        if (!result.IsAuthenticated)
        {
            return BadRequest(new ApiResponseDTO<AuthResponseDTO>
            {
                Success = false,
                Message = "Refresh token failed",
                Errors = new List<string> { result.Message ?? "Invalid refresh token" }
            });
        }

        return Ok(new ApiResponseDTO<AuthResponseDTO>
        {
            Success = true,
            Message = "Token refreshed successfully",
            Data = result
        });
    }

    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenDTO model)
    {
        var token = model.Token;

        if (string.IsNullOrWhiteSpace(token))
        {
            return BadRequest(new ApiResponseDTO<object>
            {
                Success = false,
                Message = "Revoke token failed",
                Errors = new List<string> { "Token is required" }
            });
        }

        var result = await authService.RevokeTokenAsync(token);
        if (!result.Succeeded)
        {
            return BadRequest(new ApiResponseDTO<object>
            {
                Success = false,
                Message = "Revoke token failed",
                Errors = new List<string> { result.Message }
            });
        }

        return Ok(new ApiResponseDTO<object>
        {
            Success = true,
            Message = result.Message
        });
    }


    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> Me()
    {
        var userId = User.GetUserId();

        if (string.IsNullOrWhiteSpace(userId))
            return Unauthorized(new ApiResponseDTO<object>
            {
                Success = false,
                Message = "Invalid token"
            });

        var user = await userManager.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
            return NotFound(new ApiResponseDTO<object>
            {
                Success = false,
                Message = "User not found"
            });

        var roles = await userManager.GetRolesAsync(user);

        var response = new CurrentUserDTO
        {
            Id = user.Id,
            FullName = user.FullName,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Roles = roles.ToList()
        };

        return Ok(new ApiResponseDTO<CurrentUserDTO>
        {
            Success = true,
            Message = "Current user retrieved successfully",
            Data = response
        });
    }
}