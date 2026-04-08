namespace AffaliteBL.DTOs.Auth;

public class RoleReadDTO
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? NormalizedName { get; set; }
}
