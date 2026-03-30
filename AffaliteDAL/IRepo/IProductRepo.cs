using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AffaliteDAL.Entities;

namespace AffaliteDAL.IRepo
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IQueryable<Product> GetQueryable();
    }
}
