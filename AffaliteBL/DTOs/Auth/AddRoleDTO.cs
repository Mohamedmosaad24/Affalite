using System.ComponentModel.DataAnnotations;

namespace AffaliteBL.DTOs.Auth;

public class AddRoleDTO
{
    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = string.Empty;
}
