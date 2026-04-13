
using AffaliteBL.DTOs.ReviewDTOs;

namespace AffaliteBLL.DTOs.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int SaleCount { get; set; }
        public string MerchantName { get; set; } = string.Empty;
        public decimal PlatformCommissionPct { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public List<ProductReviewDto> Reviews { get; set; } = new List<ProductReviewDto>();
        
    }
}