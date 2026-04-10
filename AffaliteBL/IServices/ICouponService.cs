using AffaliteBL.DTOs.CouponDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.IServices;
public interface ICouponService
{
    CouponResultDTO ApplyCoupon(string code, decimal orderTotal);
}
