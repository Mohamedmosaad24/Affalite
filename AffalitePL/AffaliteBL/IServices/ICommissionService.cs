using AffaliteBL.DTOs.CommissionDTOs;
using AffaliteDAL.Entities.Enums;

namespace AffaliteBL.IServices
{
    public interface ICommissionService
    {
      
        IEnumerable<CommissionReadDTO> GetCommissionsByAffiliate(int affiliateId);

     
        CommissionReadDTO GetCommissionByOrderId(int orderId);

      
        void UpdateCommissionStatus(int id, CommissionStatus status);

     
        void CalculateAndSaveCommission(int orderId, decimal totalPrice, decimal pct);
    }
}
