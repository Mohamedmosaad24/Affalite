using AffaliteDAL.Entities;

namespace AffaliteDAL.IRepo
{
    public interface IAiContentRepo : IGenericRepository<AiContentHistory>
    {
        IEnumerable<AiContentHistory> GetByAffiliateId(int affiliateId, int page = 1, int pageSize = 20);
        AiContentHistory? GetByAffiliateAndProduct(int affiliateId, int productId);
        Task AddAsync(AiContentHistory entity);
        Task<int> SaveChangesAsync();
    }
}