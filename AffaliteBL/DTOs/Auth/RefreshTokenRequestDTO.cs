using System.ComponentModel.DataAnnotations;

namespace AffaliteBL.DTOs.Auth;

public class RefreshTokenRequestDTO
{
    [Required]
    public string Token { get; set; } = string.Empty;
}
