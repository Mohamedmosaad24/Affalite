using AffaliteBL.DTOs.AiDTOs;

namespace AffaliteBL.IServices
{
    public interface IAiContentService
    {
        Task<AiContentResponse> GenerateContentAsync(ContentGenerationRequest request);
        Task<IEnumerable<AiContentHistoryDTO>> GetAffiliateContentHistoryAsync(int affiliateId, int page = 1, int pageSize = 20);
        Task<bool> SaveContentAsync(int affiliateId, int productId, string content, string contentType);
    }
}