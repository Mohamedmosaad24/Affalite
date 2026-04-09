using System;

namespace AffaliteDAL.Entities
{
    public class Coupon
    {
        public int Id { get; set; }                     // Coupon Id
        public string Code { get; set; } = string.Empty; // Coupon code
        public decimal DiscountAmount { get; set; }    // Fixed discount amount
        public decimal? DiscountPercentage { get; set; } // Optional percentage discount
        public DateTime ValidFrom { get; set; }        // Start date
        public DateTime ValidTo { get; set; }          // Expiration date
        public bool IsActive { get; set; } = true;     // Is coupon active
        public int? MaxUsageCount { get; set; }        // Optional maximum number of uses
        public int? UsedCount { get; set; } = 0;      // Track how many times it was used
        public int? ProductId { get; set; }            // Optional: restrict to a specific product

        // Navigation property
        public Product? Product { get; set; }
    }
}