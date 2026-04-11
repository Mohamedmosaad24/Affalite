using AffaliteBL.DTOs.ReviewDTOs;
using AffaliteBLL.DTOs;
using AffaliteDAL.Entities;

namespace AffaliteBLL.Services.Interfaces
{
    public interface IProductReviewService
    {
        IEnumerable<ProductReviewDto> GetAll();

        ProductReviewDto? GetById(int id);

        IEnumerable<ProductReviewDto> GetByProductId(int productId);

        IEnumerable<ProductReviewDto> GetByAffiliateId(int affiliateId);

        ProductReviewDto Create(CreateProductReviewDto dto);

        bool Update(int id, ProductReviews review);
        bool Delete(int id);
        double GetAverageRating(int productId);
    }
}