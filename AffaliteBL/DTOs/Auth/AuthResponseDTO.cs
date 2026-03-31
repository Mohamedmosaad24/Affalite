using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.DTOs.Auth;
public class AuthResponseDTO
{
    public string? Message { get; set; }
    public bool IsAuthenticated { get; set; }
    public string? UserId { get; set; }
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Token { get; set; }
    public DateTime Expiration { get; set; }
    public List<string> Roles { get; set; } = new();

    public string? RefreshToken { get; set; }

    public DateTime RefreshTokenExpiration { get; set; }
}
