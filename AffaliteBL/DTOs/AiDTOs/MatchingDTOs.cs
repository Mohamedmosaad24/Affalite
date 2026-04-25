using System.ComponentModel.DataAnnotations;

namespace AffaliteBL.DTOs.MatchingDTOs
{
    public class MatchRecommendationDTO
    {
        public int MatchId { get; set; }
        public int TargetId { get; set; } // MerchantId or AffiliateId
        public string TargetName { get; set; } = string.Empty;
        public string TargetType { get; set; } = string.Empty; // "Merchant" or "Affiliate"
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal MatchScore { get; set; }
        public string MatchReason { get; set; } = string.Empty;
        public decimal? ExpectedCommission { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class MatchResponseRequest
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public bool IsAccepted { get; set; }
    }
}