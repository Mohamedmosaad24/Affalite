using AffaliteDAL.Entities;

namespace AffaliteDAL.IRepo
{
    public interface IMatchingRepo : IGenericRepository<AffiliateMerchantMatch>
    {
        IEnumerable<AffiliateMerchantMatch> GetPendingMatchesByAffiliate(int affiliateId);
        IEnumerable<AffiliateMerchantMatch> GetPendingMatchesByMerchant(int merchantId);
        AffiliateMerchantMatch? GetMatchById(int matchId);
        Task AddAsync(AffiliateMerchantMatch entity);
        Task<int> SaveChangesAsync();
    }
}