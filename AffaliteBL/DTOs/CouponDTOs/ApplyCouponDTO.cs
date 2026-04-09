using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.DTOs.CouponDTOs;
public class ApplyCouponDTO
{
    public string Code { get; set; } = string.Empty;
    public decimal OrderTotal { get; set; }
}
