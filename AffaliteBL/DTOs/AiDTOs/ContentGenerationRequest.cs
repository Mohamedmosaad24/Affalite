using System.ComponentModel.DataAnnotations;

namespace AffaliteBL.DTOs.AiDTOs
{
    public class ContentGenerationRequest
    {
        [Required]
        public int AffiliateId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [StringLength(50)]
        public string ContentType { get; set; } = "social_post";

        [StringLength(50)]
        public string Platform { get; set; } = "facebook";

        [StringLength(20)]
        public string Tone { get; set; } = "friendly";

        [StringLength(10)]
        public string Language { get; set; } = "ar";

        [StringLength(500)]
        public string? CustomInstructions { get; set; }
    }

    public class AiContentResponse
    {
        public string Content { get; set; } = string.Empty;
        public int TokensUsed { get; set; }
        public decimal EstimatedCost { get; set; }
        public string? Warning { get; set; }
    }

    public class AiContentHistoryDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public string GeneratedContent { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
