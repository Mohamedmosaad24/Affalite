using System;
using System.Collections.Generic;

namespace AffaliteDAL.Entities
{
    public class Wishlist
    {
        public int Id { get; set; }              // Wishlist entry Id
        public int AffiliateId { get; set; }          // User who owns the wishlist
        public int ProductId { get; set; }       // Product added to wishlist
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Product? Product { get; set; }
        // optionally, if you have a User entity
        // public User? User { get; set; }
    }
}