using AffaliteDAL.Entities;
namespace AffaliteDAL.IRepo
{
    public interface IAffiliateRepo : IGenericRepository<Affiliate>
    {
        IEnumerable<Order> GetAffiliateOrders(int affiliateId);
        IEnumerable<Commission> GetAffiliateCommissions(int affiliateId);
        decimal? GetAffiliateBalance(int affiliateId);

    }
}
