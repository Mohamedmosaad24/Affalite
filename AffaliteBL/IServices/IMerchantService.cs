using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AffaliteBL.DTOs.MerchantDTOs;
using AffaliteBL.DTOs.OrderDTOs;
using AffaliteDAL.Entities;

namespace AffaliteBL.IServices
{
    public interface IMerchantService
    {
        IEnumerable<Merchant> GetAllMerchants();
        Merchant? GetMerchantById(int id);
        void CreateMerchant(Merchant merchant);
        void UpdateMerchant(Merchant merchant);
        void DeleteMerchant(int id);
        IEnumerable<GetMerchantDTO> GetAllMerchantsWithDetails();

        // Relations
        IEnumerable<OrderReadDTO> GetMerchantOrders(int merchantId);
        MerchantBalanceDTO? GetMerchantBalance(int merchantId);
        Merchant? GetMerchantByUserId(string userId);

    }
}
