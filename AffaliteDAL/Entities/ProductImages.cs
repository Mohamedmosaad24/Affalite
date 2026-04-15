using System.Text.Json.Serialization;

namespace AffaliteDAL.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        [JsonIgnore]
        public Product? Product { get; set; }
    }
}