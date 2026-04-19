using AffaliteBL.DTOs.CommissionDTOs;
using AffaliteDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AffaliteBL.IServices
{
    public interface ICommissionService
    {
        IEnumerable<CommissionReadDTO> GetCommissionsByAffiliate(int affiliateId);
        IEnumerable<CommissionReadDTO> GetCommissionsByMerchant(int merchantId);

        CommissionReadDTO GetCommissionByOrderId(int orderId);

        void UpdateCommissionStatus(int id, CommissionStatus status);

        void CalculateAndSaveCommission(int orderId, decimal totalPrice, decimal pct);

    }
}
