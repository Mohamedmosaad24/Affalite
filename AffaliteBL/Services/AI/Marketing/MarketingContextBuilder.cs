using AffaliteDAL.Entities;

namespace AffaliteBL.Services.AI.Marketing
{
    public class MarketingContextBuilder : IMarketingContextBuilder
    {
        public MarketingProductContext Build(Product product)
        {
            var reviews = product.Reviews?.ToList() ?? new List<ProductReviews>();
            var ratingCount = reviews.Count;
            var avgRating = ratingCount == 0 ? 0 : (decimal)reviews.Average(r => r.Rating);

            return new MarketingProductContext
            {
                ProductId = product.Id,
                ProductName = product.Name ?? string.Empty,
                Description = product.Description ?? string.Empty,
                Details = product.Details ?? string.Empty,
                Price = product.Price,
                CategoryName = product.Category?.Name ?? string.Empty,
                SaleCount = product.SaleCount,
                ReviewsCount = ratingCount,
                AverageRating = Math.Round(avgRating, 2),
                TopReviewHighlights = reviews
                    .Where(r => !string.IsNullOrWhiteSpace(r.Comment))
                    .OrderByDescending(r => r.Rating)
                    .ThenByDescending(r => r.CreatedAt)
                    .Take(3)
                    .Select(r => r.Comment.Trim())
                    .ToList()
            };
        }
    }
}
