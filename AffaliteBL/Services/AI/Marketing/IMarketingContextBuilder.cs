using AffaliteDAL.Entities;

namespace AffaliteBL.Services.AI.Marketing
{
    public interface IMarketingContextBuilder
    {
        MarketingProductContext Build(Product product);
    }
}
