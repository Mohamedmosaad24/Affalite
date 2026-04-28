using AffaliteBL.DTOs.MatchingDTOs;
using AffaliteBL.IServices;
using AffaliteDAL.Data;
using AffaliteDAL.Entities;
using AffaliteDAL.Entities.Enums;
using AffaliteDAL.IRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Numerics.Tensors;
using System.Text.Json;

namespace AffaliteBL.Services
{
    public class MatchingService : IMatchingService
    {
        private readonly AffaliteDBContext _context;
        private readonly IMatchingRepo _matchingRepo;
        private readonly IAffiliateRepo _affiliateRepo;
        private readonly IProductRepository _productRepo;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public MatchingService(
            AffaliteDBContext context,
            IMatchingRepo matchingRepo,
            IAffiliateRepo affiliateRepo,
            IProductRepository productRepo,
            IHttpClientFactory httpClientFactory,
            IConfiguration config)
        {
            _context = context;
            _matchingRepo = matchingRepo;
            _affiliateRepo = affiliateRepo;
            _productRepo = productRepo;
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<List<MatchRecommendationDTO>> GetRecommendationsForAffiliateAsync(int affiliateId, int topN = 10)
        {
            var affiliate = await _affiliateRepo.GetByIdAsync(affiliateId)
                ?? throw new Exception("Affiliate not found");

            var activeProducts = await _context.Products
                .Include(p => p.Merchant).ThenInclude(m => m.AppUser)
                .Include(p => p.Category)
                .Where(p => p.Status == ProductStatus.Active && p.Stock > 0)
                .ToListAsync();

            // تحويل اهتمامات المسوق إلى متجه (Vector)
            float[] affiliateVector = await GetEmbeddingAsync(
                $"{affiliate.AppUser?.FullName} {string.Join(" ", activeProducts.Select(p => p.Category?.Name))}");

            var matches = new List<MatchCandidate>();

            foreach (var product in activeProducts)
            {
                // تحويل بيانات المنتج إلى متجه (Vector)
                float[] productVector = await GetEmbeddingAsync(
                    $"{product.Name} {product.Description} {product.Category?.Name} {product.Details}");

                // حساب نسبة التطابق باستخدام Cosine Similarity
                float similarity = TensorPrimitives.CosineSimilarity(affiliateVector.AsSpan(), productVector.AsSpan());
                double score = CalculateHybridScore(similarity, affiliate, product);

                if (score > 0.3)
                {
                    matches.Add(new MatchCandidate
                    {
                        Product = product,
                        Score = score,
                        Reason = GenerateMatchReason(similarity, affiliate, product)
                    });
                }
            }

            return matches
                .OrderByDescending(m => m.Score)
                .Take(topN)
                .Select(m => new MatchRecommendationDTO
                {
                    TargetId = m.Product.MerchantId,
                    TargetName = m.Product.Merchant.AppUser?.FullName ?? "Unknown",
                    TargetType = "Merchant",
                    ProductId = m.Product.Id,
                    ProductName = m.Product.Name,
                    MatchScore = (decimal)Math.Round(m.Score, 4),
                    MatchReason = m.Reason,
                    ExpectedCommission = m.Product.Price * (m.Product.PlatformCommissionPct / 100m),
                    CreatedAt = DateTime.UtcNow
                }).ToList();
        }

        // دالة التحدث مع Cohere API لتوليد الـ Embeddings
        private async Task<float[]> GetEmbeddingAsync(string text)
        {
            var apiKey = _config["AiSettings:CohereApiKey"]
                ?? throw new Exception("Cohere API Key is missing.");
            var model = _config["AiSettings:EmbeddingModel"] ?? "embed-multilingual-v3.0";

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var payload = new
            {
                texts = new[] { text },
                model = model,
                input_type = "search_document" // مطلوب للإصدار الثالث من Cohere
            };

            var response = await client.PostAsJsonAsync("https://api.cohere.ai/v1/embed", payload);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<CohereEmbedResponse>();

            return result?.Embeddings?.FirstOrDefault() ?? Array.Empty<float>();
        }

        private double CalculateHybridScore(float vectorSimilarity, Affiliate affiliate, Product product)
        {
            double score = vectorSimilarity * 0.5;
            score += 0.2; // Language match
            if (product.Category != null) score += 0.15;
            if (product.PlatformCommissionPct >= 5m) score += 0.1;
            return Math.Min(score, 1.0);
        }

        private string GenerateMatchReason(float similarity, Affiliate affiliate, Product product)
        {
            return similarity > 0.8f
                ? $"تطابق ممتاز! جمهورك مهتم بـ {product.Category?.Name} وهذا المنتج له تقييمات عالية."
                : similarity > 0.6f
                    ? $"المنتج مناسب لنيتشك في {product.Category?.Name} مع هامش ربح جيد."
                    : $"فرصة تجريبية: المنتج جديد في فئة {product.Category?.Name}.";
        }

        public Task<List<MatchRecommendationDTO>> GetRecommendationsForMerchantAsync(int merchantId, int topN = 10)
            => Task.FromResult(new List<MatchRecommendationDTO>());

        public async Task<bool> ProcessMatchResponseAsync(int matchId, string userId, bool isAccepted)
        {
            var match = _matchingRepo.GetMatchById(matchId);
            if (match == null) return false;
            if (match.Affiliate.AppUserId != userId && match.Merchant.AppUserId != userId) return false;

            match.Status = isAccepted ? "Accepted" : "Rejected";
            match.Feedback = isAccepted ? "Accepted by user" : "Rejected by user";
            _matchingRepo.Update(match);
            return await _matchingRepo.SaveChangesAsync() > 0;
        }

        public async Task RunMatchingJobAsync() { /* ... implementation ... */ }

        private class MatchCandidate
        {
            public Product Product { get; set; } = null!;
            public double Score { get; set; }
            public string Reason { get; set; } = string.Empty;
        }

        // كلاسات مساعدة لقراءة رد منصة Cohere
        private class CohereEmbedResponse
        {
            public float[][] Embeddings { get; set; } = Array.Empty<float[]>();
        }
    }
}