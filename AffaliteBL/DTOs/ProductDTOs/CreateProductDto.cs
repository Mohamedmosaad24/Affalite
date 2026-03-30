using Microsoft.AspNetCore.Http;

namespace AffaliteBLL.DTOs.Products
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public IFormFile Image { get; set; } = null!;

        public int MerchantId { get; set; }
        public decimal PlatformCommissionPct { get; set; }
    }
}