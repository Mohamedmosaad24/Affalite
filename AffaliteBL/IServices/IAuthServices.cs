using AffaliteBL.DTOs.Auth;
using AffaliteDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.IServices;

public interface IAuthServices
{
    Task<AuthResponseDTO> RegisterAffiliateAsync(RegisterDTO model);
    Task<AuthResponseDTO> RegisterMerchantAsync(RegisterDTO model);
    Task<AuthResponseDTO> LoginAsync(LoginDTO model);
    Task<ActionResponseDTO> AddRoleAsync(AddRoleDTO model);
    Task<ActionResponseDTO> ChangePasswordAsync(string userId, ChangePasswordDTO model);
    Task<List<RoleReadDTO>> GetRolesAsync();
    Task<AuthResponseDTO> RefreshTokenAsync(string token);
    Task<ActionResponseDTO> RevokeTokenAsync(string token);
}