using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AffaliteDAL.Entities
{
    [Table("AffiliateMerchantMatches")]
    public class AffiliateMerchantMatch
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MerchantId { get; set; }

        [Required]
        public int AffiliateId { get; set; }

        public int? ProductId { get; set; } // لو المطابقة لمنتج محدد

        [Column(TypeName = "decimal(5,4)")]
        public decimal MatchScore { get; set; } // 0.0000 to 1.0000

        [Column(TypeName = "nvarchar(max)")]
        public string? MatchReason { get; set; } // شرح سبب التوصية

        [StringLength(20)]
        public string Status { get; set; } = "Pending"; // Pending, Accepted, Rejected, Expired

        [Column(TypeName = "nvarchar(max)")]
        public string? Feedback { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ExpiredAt { get; set; }

        // Navigation Properties
        [ForeignKey("MerchantId")]
        public Merchant Merchant { get; set; } = null!;

        [ForeignKey("AffiliateId")]
        public Affiliate Affiliate { get; set; } = null!;

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}