using AffaliteDAL.Data;
using AffaliteDAL.Entities;
using AffaliteDAL.IRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteDAL.Repo
{
    public class AffiliateRepo : GenericRepository<Affiliate>, IAffiliateRepo
    {
        private readonly AffaliteDBContext _context;
        public AffiliateRepo(AffaliteDBContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAffiliateOrders(int affiliateId)
        {
            return _context.Orders
                .Where(o => o.AffiliateId == affiliateId)
                .ToList();
        }

        public IEnumerable<Commission> GetAffiliateCommissions(int affiliateId)
        {
            return _context.Commissions
                .Include(c => c.Order)
                .Where(c => c.Order.AffiliateId == affiliateId)
                .ToList();
        }

        public decimal? GetAffiliateBalance(int affiliateId)
        {
            return _context.Affiliates
                .Where(a => a.Id == affiliateId)
                .Select(a => (decimal?)a.Balance)
                .FirstOrDefault();
        }
         
        public Affiliate? GetAffiliateUserId(string userId)
        {
            return _context.Affiliates
    .Where(a => a.AppUserId == userId)
    .FirstOrDefault();
        }
    }
}
