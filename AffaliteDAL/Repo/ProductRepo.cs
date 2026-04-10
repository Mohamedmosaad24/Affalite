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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AffaliteDBContext _context;

        public ProductRepository(AffaliteDBContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Product> GetQueryable()
        {
            return _context.Products
                .Include(p => p.Category)
                .Include(p => p.Merchant).ThenInclude(p=>p.AppUser)
                .Include(p=>p.Images)
                .Include(p=>p.Reviews)
                .AsQueryable();
        }
    }
}
