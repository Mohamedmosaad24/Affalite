using AffaliteBL.DTOs.Auth;
using AffaliteDAL.Entities;

namespace AffaliteBL.IServices;

public interface IJwtServices
{
    Task<TokenResultDTO> GenerateTokenAsync(AppUser user);
}