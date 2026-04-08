using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteDAL.Entities;

public class AppUser : IdentityUser
{
    public string FullName { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; } = new();

    public Merchant? MerchantProfile { get; set; }
    public Affiliate? AffiliateProfile { get; set; }
}
