using AffaliteBL.DTOs.CouponDTOs;
using AffaliteBL.IServices;
using AffaliteDAL.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.Services;
public class CouponService:ICouponService
{
    private readonly ICouponRepository _couponRepository;
    public CouponService(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }

    public CouponResultDTO ApplyCoupon(string code, decimal orderTotal)
    {
        var coupon = _couponRepository.GetByCode(code);

        // كوبون مش موجود
        if (coupon == null)
            return new CouponResultDTO { IsValid = false, Message = "Coupon not found" };

        // كوبون مش active
        if (!coupon.IsActive)
            return new CouponResultDTO { IsValid = false, Message = "Coupon is not active" };

        // كوبون منتهي
        if (DateTime.Now < coupon.ValidFrom || DateTime.Now > coupon.ValidTo)
            return new CouponResultDTO { IsValid = false, Message = "Coupon has expired" };

        // وصل الحد الأقصى
        if (coupon.MaxUsageCount.HasValue && coupon.UsedCount >= coupon.MaxUsageCount)
            return new CouponResultDTO { IsValid = false, Message = "Coupon usage limit reached" };

        // احسب الخصم
        decimal discount = 0;
        if (coupon.DiscountPercentage.HasValue)
            discount = orderTotal * (coupon.DiscountPercentage.Value / 100);
        else
            discount = coupon.DiscountAmount;

        // زوّد الـ UsedCount
        coupon.UsedCount = (coupon.UsedCount ?? 0) + 1;
        _couponRepository.Update(coupon);
        _couponRepository.Save();

        return new CouponResultDTO
        {
            IsValid = true,
            Message = "Coupon applied successfully",
            DiscountAmount = discount,
            FinalTotal = orderTotal - discount
        };
    }
}
