using AffaliteDAL.Entities.Enums;

namespace AffaliteBLL.DTOs.Products
{
    public class UpdateProductDto : CreateProductDto
    {
        public ProductStatus Status { get; set; }
        public List<string> ExistingImageUrls { get; set; } = new();
    }
}