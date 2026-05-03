using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AffaliteDAL.Entities;

namespace AffaliteDAL.IRepo
{
    public interface IMerchantRepo : IGenericRepository<Merchant>
    {
        IEnumerable<Product> GetMerchantProducts(int merchantId);
        IEnumerable<Order> GetMerchantOrders(int merchantId);
        decimal? GetMerchantBalance(int merchantId);
        Merchant? GetMerchantByUserId(string userId);
        Merchant? GetByIdWithUser(int id);
        public IEnumerable<Merchant> GetAllMerchants();
        IEnumerable<Merchant> GetAllMerchantsWithDetails();
        decimal GetMerchantCommissionTotal(int merchantId);

    


    }
}
