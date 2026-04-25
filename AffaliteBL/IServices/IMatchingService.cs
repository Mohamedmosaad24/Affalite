using AffaliteBL.DTOs.MatchingDTOs;

namespace AffaliteBL.IServices
{
    public interface IMatchingService
    {
        Task<List<MatchRecommendationDTO>> GetRecommendationsForAffiliateAsync(int affiliateId, int topN = 10);
        Task<List<MatchRecommendationDTO>> GetRecommendationsForMerchantAsync(int merchantId, int topN = 10);
        Task<bool> ProcessMatchResponseAsync(int matchId, string userId, bool isAccepted);
        Task RunMatchingJobAsync();
    }
}