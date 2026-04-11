using AffaliteBL.DTOs.ReviewDTOs;
using AffaliteBLL.DTOs;
using AffaliteBLL.Services.Interfaces;
using AffaliteDAL.Entities;
using AffaliteDAL.IRepo;

namespace AffaliteBLL.Services
{
    public class ProductReviewService : IProductReviewService
    {
        private readonly IProductReviewRepo _repo;

        public ProductReviewService(IProductReviewRepo repo)
        {
            _repo = repo;
        }

        public IEnumerable<ProductReviewDto> GetAll()
        {
            var reviews = _repo.GetAllQueryable()
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new ProductReviewDto
                {
                    Id = r.Id,
                    ProductId = r.ProductId,
                    AffiliateId = r.AffiliateId,
                    Comment = r.Comment,
                    Rating = r.Rating,
                    CreatedAt = r.CreatedAt
                })
                .ToList();

            return reviews;
        }

        public ProductReviewDto? GetById(int id)
        {
            var review = _repo.GetById(id);

            if (review == null)
                return null;

            return new ProductReviewDto
            {
                Id = review.Id,
                ProductId = review.ProductId,
                AffiliateId = review.AffiliateId,
                Comment = review.Comment,
                Rating = review.Rating,
                CreatedAt = review.CreatedAt
            };
        }

        public IEnumerable<ProductReviewDto> GetByProductId(int productId)
        {
            var reviews = _repo.GetAllQueryable()
                .Where(r => r.ProductId == productId)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new ProductReviewDto
                {
                    Id = r.Id,
                    ProductId = r.ProductId,
                    AffiliateId = r.AffiliateId,
                    Comment = r.Comment,
                    Rating = r.Rating,
                    CreatedAt = r.CreatedAt
                })
                .ToList();

            return reviews;
        }

        public IEnumerable<ProductReviewDto> GetByAffiliateId(int affiliateId)
        {
            var reviews = _repo.GetAllQueryable()
                .Where(r => r.AffiliateId == affiliateId)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new ProductReviewDto
                {
                    Id = r.Id,
                    ProductId = r.ProductId,
                    AffiliateId = r.AffiliateId,
                    Comment = r.Comment,
                    Rating = r.Rating,
                    CreatedAt = r.CreatedAt
                })
                .ToList();

            return reviews;
        }

        public ProductReviewDto Create(CreateProductReviewDto dto)
        {
            if (dto.Rating < 1 || dto.Rating > 5)
                throw new ArgumentException("Rating must be between 1 and 5");

            var exists = _repo.GetAllQueryable()
                .Any(r => r.ProductId == dto.ProductId && r.AffiliateId == dto.AffiliateId);
            if (!exists)
                throw new ArgumentException("Product does not exist");

            var review = new ProductReviews
            {
                ProductId = dto.ProductId,
                AffiliateId = dto.AffiliateId,
                Comment = dto.Comment,
                Rating = dto.Rating,
                CreatedAt = DateTime.UtcNow
            };

            _repo.Add(review);
            _repo.SaveChanges();

            return new ProductReviewDto
            {
                Id = review.Id,
                ProductId = review.ProductId,
                AffiliateId = review.AffiliateId,
                Comment = review.Comment,
                Rating = review.Rating,
                CreatedAt = review.CreatedAt
            };
        }

        public bool Update(int id, ProductReviews review)
        {
            if (review.Rating < 1 || review.Rating > 5)
                throw new ArgumentException("Rating must be between 1 and 5");

            var existing = _repo.GetById(id);
            if (existing == null)
                return false;

            existing.Comment = review.Comment;
            existing.Rating = review.Rating;

            _repo.Update(existing, id);
            _repo.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var existing = _repo.GetById(id);
            if (existing == null)
                return false;

            _repo.Delete(existing, id);
            _repo.SaveChanges();

            return true;
        }

        public double GetAverageRating(int productId)
        {
            var avg = _repo.GetAllQueryable()
                .Where(r => r.ProductId == productId)
                .Select(r => (double?)r.Rating)
                .Average();

            return avg ?? 0;
        }
    }
}