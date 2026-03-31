using AffaliteBL.DTOs.Auth;
using AffaliteBL.IServices;
using AffaliteDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AffaliteBL.Services;

public class JwtServices : IJwtServices
{
    private readonly JwtOptions _options;
    private readonly UserManager<AppUser> _userManager;

    public JwtServices(IOptions<JwtOptions> options, UserManager<AppUser> userManager)
    {
        _options = options.Value;
        _userManager = userManager;
    }

    public async Task<TokenResultDTO> GenerateTokenAsync(AppUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = (await _userManager.GetRolesAsync(user)).ToList();

        var roleClaims = roles.Select(role => new Claim("role", role));

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new("uid", user.Id)
        };

        claims.AddRange(userClaims);
        claims.AddRange(roleClaims);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiresOn = DateTime.UtcNow.AddMinutes(_options.DurationInMinutes);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: expiresOn,
            signingCredentials: signingCredentials);

        return new TokenResultDTO
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            ExpiresOn = expiresOn,
            Roles = roles
        };
    }
}
