using System;

namespace AffaliteBLL.DTOs
{
    public class CreateProductReviewDto
    {
        public int ProductId { get; set; }     // Associated Product
        public int AffiliateId { get; set; }        // User who made the review
        public string Comment { get; set; } = string.Empty; // Review text
        public int Rating { get; set; }        // 1-5 rating
    }
}