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
    public class CommissionRepo : GenericRepository<Commission>, ICommissionRepo
    {
        private readonly AffaliteDBContext _context;
    
            public CommissionRepo(AffaliteDBContext context) : base(context)
        {
                _context = context;
            }

        //public IEnumerable<Commission> GetAllCommissionsByMerchant(int merchantId)
        //{
        //    var merchant = _context.Merchants.FirstOrDefault(m=>m.Id == merchantId);
        //    return _context.Commissions
        //      .Include(c => c.Order)
        //      .ThenInclude(mc => mc.MerchantOrder).ThenInclude(mo => mo.Merchant)
        //    .Where(o => o.MerchantCommissions.Any(m => m.MerchantId == merchantId)) // ← Any() لأنها Collection
        //      .ToList();
        //}

        public IEnumerable<Commission> GetAllCommissionsByMerchant(int merchantId)
        {
            var merchant = _context.Merchants.FirstOrDefault(m => m.Id == merchantId);
            if (merchant == null) return Enumerable.Empty<Commission>(); // ✅ تأمين

            return _context.Commissions
                .Include(c => c.MerchantCommissions) // ✅ هنا كانت ناقصة
                .Include(c => c.Order)
                    .ThenInclude(o => o.MerchantOrder)
                        .ThenInclude(mo => mo.Merchant)
                .Where(c => c.MerchantCommissions.Any(m => m.MerchantId == merchantId))
                .ToList();
        }
    }
}
