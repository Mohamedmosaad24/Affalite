using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AffaliteDAL.Entities
{
    [Table("AiContentHistory")]
    public class AiContentHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AffiliateId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? PromptText { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? GeneratedContent { get; set; }

        [StringLength(50)]
        public string ContentType { get; set; } = "social_post"; // social_post, email, ad_copy

        [StringLength(50)]
        public string Platform { get; set; } = "facebook"; // facebook, instagram, tiktok, email

        [StringLength(20)]
        public string Language { get; set; } = "ar"; // ar, en

        [StringLength(20)]
        public string Tone { get; set; } = "friendly"; // professional, friendly, urgent

        public int? TokensUsed { get; set; }

        [Column(TypeName = "decimal(10,6)")]
        public decimal? Cost { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Generated"; // Generated, Published, Discarded

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("AffiliateId")]
        public Affiliate Affiliate { get; set; } = null!;

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;
    }
}
