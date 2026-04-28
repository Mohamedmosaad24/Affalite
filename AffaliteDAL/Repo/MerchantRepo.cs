using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AffaliteDAL.Data;
using AffaliteDAL.Entities;
using AffaliteDAL.IRepo;
using Microsoft.EntityFrameworkCore;

namespace AffaliteDAL.Repo
{
    public class MerchantRepo : GenericRepository<Merchant>, IMerchantRepo
    {
        private readonly AffaliteDBContext _context;

        public MerchantRepo(AffaliteDBContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Merchant> GetAllMerchants()
        {
            return _context.Merchants
                .Include(m => m.AppUser) // Include related AppUser data if needed
                .ToList();
        }

        public IEnumerable<Product> GetMerchantProducts(int merchantId)
        {
            return _context.Products
                .Where(p => p.MerchantId == merchantId)
                .ToList();
        }

        public IEnumerable<Order> GetMerchantOrders(int merchantId)
        {
            return _context.Orders.Include(m=>m.Items).ThenInclude(o=>o.Product)
                .Where(o => o.Id == merchantId)
                .ToList();
        }

        public decimal? GetMerchantBalance(int merchantId)
        {
            return _context.Merchants
                .Where(m => m.Id == merchantId)
                .Select(m => (decimal?)m.Balance)
                .FirstOrDefault();
        }
        
        public Merchant? GetMerchantByUserId(string userId)
        {
            return _context.Merchants.Where(m => m.AppUserId == userId).FirstOrDefault();
        }

      
    }
}