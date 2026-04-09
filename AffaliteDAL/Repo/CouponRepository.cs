using AffaliteDAL.Data;
using AffaliteDAL.Entities;
using AffaliteDAL.IRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteDAL.Repo;
public class CouponRepository:ICouponRepository
{
    private readonly AffaliteDBContext _context;
    

    public CouponRepository(AffaliteDBContext context)
    {
        _context = context;
        
    }


    public Coupon? GetByCode(string code)
        {
            return _context.Coupons.FirstOrDefault(c => c.Code == code);
        }

        public void Update(Coupon coupon)
        {
            _context.Coupons.Update(coupon);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
