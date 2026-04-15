using AffaliteBL.DTOs.AffiliateDTOs;
using AffaliteBL.DTOs.CommissionDTOs;
using AffaliteBL.DTOs.OrderDTOs;
using AffaliteDAL.Entities;

namespace AffaliteBL.IServices
{
    public interface IAffiliateService 
    {
        IEnumerable<Affiliate> GetAllAffiliates();
        Affiliate? GetAffiliateById(int id);
        void CreateAffiliate(Affiliate affiliate);
        void UpdateAffiliate(Affiliate affiliate);
        void DeleteAffiliate(int id);
        /// Additional methods related to orders and commissions can be added here, for example:
        IEnumerable<OrderReadDTO> GetAffiliateOrders(int affiliateId);
        IEnumerable<CommissionReadDTO> GetAffiliateCommissions(int affiliateId);
        AffiliateBalanceDTO? GetAffiliateBalance(int affiliateId);
        Affiliate? GetAffiliateUserId(string userId);

    }
}