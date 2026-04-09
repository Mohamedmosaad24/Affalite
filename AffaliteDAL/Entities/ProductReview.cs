using AffaliteDAL.Entities.Enums;

namespace AffaliteDAL.Entities
{
    public class ProductReview
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int AffiliateId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; } // 1-5 scale
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Product? Product { get; set; }
        // optionally, if you have a User entity
        // public User? User { get; set; }
    }
}