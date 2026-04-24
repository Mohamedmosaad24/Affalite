using AffaliteBL.DTOs.AiDTOS;
using AffaliteDAL.Entities;

namespace AffaliteBL.IServices
{
    public interface IMarketingService
    {
        Task<MarketingPostsDto> GeneratePostsAsync(Product product, MarketingGenerationRequestDto? request = null);
        Task InvalidateCacheAsync(int productId);
    }
}