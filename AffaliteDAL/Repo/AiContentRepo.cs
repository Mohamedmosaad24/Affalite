using AffaliteDAL.Data;
using AffaliteDAL.Entities;
using AffaliteDAL.IRepo;
using Microsoft.EntityFrameworkCore;

namespace AffaliteDAL.Repo
{
    public class AiContentRepo : GenericRepository<AiContentHistory>, IAiContentRepo
    {
        private readonly AffaliteDBContext _context;

        public AiContentRepo(AffaliteDBContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<AiContentHistory> GetByAffiliateId(int affiliateId, int page = 1, int pageSize = 20)
        {
            return _context.AiContentHistories
                .Include(h => h.Product)
                .Where(h => h.AffiliateId == affiliateId)
                .OrderByDescending(h => h.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public AiContentHistory? GetByAffiliateAndProduct(int affiliateId, int productId)
        {
            return _context.AiContentHistories
                .FirstOrDefault(h => h.AffiliateId == affiliateId && h.ProductId == productId);
        }

        public async Task AddAsync(AiContentHistory entity)
        {
            await _context.AiContentHistories.AddAsync(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
