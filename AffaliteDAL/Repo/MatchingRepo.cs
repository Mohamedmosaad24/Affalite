using AffaliteDAL.Data;
using AffaliteDAL.Entities;
using AffaliteDAL.IRepo;
using Microsoft.EntityFrameworkCore;

namespace AffaliteDAL.Repo
{
    public class MatchingRepo : GenericRepository<AffiliateMerchantMatch>, IMatchingRepo
    {
        private readonly AffaliteDBContext _context;

        public MatchingRepo(AffaliteDBContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<AffiliateMerchantMatch> GetPendingMatchesByAffiliate(int affiliateId)
        {
            return _context.AffiliateMerchantMatches
                .Include(m => m.Merchant).ThenInclude(mt => mt.AppUser)
                .Include(m => m.Product)
                .Where(m => m.AffiliateId == affiliateId && m.Status == "Pending")
                .OrderByDescending(m => m.CreatedAt)
                .ToList();
        }

        public IEnumerable<AffiliateMerchantMatch> GetPendingMatchesByMerchant(int merchantId)
        {
            return _context.AffiliateMerchantMatches
                .Include(m => m.Affiliate).ThenInclude(af => af.AppUser)
                .Include(m => m.Product)
                .Where(m => m.MerchantId == merchantId && m.Status == "Pending")
                .OrderByDescending(m => m.CreatedAt)
                .ToList();
        }

        public AffiliateMerchantMatch? GetMatchById(int matchId)
        {
            return _context.AffiliateMerchantMatches
                .Include(m => m.Merchant)
                .Include(m => m.Affiliate)
                .Include(m => m.Product)
                .FirstOrDefault(m => m.Id == matchId);
        }

        public async Task AddAsync(AffiliateMerchantMatch entity)
        {
            await _context.AffiliateMerchantMatches.AddAsync(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}