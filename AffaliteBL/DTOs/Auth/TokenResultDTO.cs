namespace AffaliteBL.DTOs.Auth;

public class TokenResultDTO
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresOn { get; set; }
    public List<string> Roles { get; set; } = new();
}
