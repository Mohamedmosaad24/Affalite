using Microsoft.AspNetCore.Http;

namespace AffaliteBLL.DTOs.Products
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public ICollection<IFormFile> Images { get; set; } = new List<IFormFile>();
        public int MerchantId { get; set; }
        public decimal PlatformCommissionPct { get; set; }
    }
}