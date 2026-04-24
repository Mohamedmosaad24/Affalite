using AffaliteBL.DTOs.AiDTOS;
using AffaliteBL.IServices;
using AffaliteBL.Services.AI.Marketing;
using AffaliteDAL.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace AffaliteBL.Services
{
    public class MarketingService : IMarketingService
    {
        private readonly IDistributedCache _cache;
        private readonly IMarketingContextBuilder _contextBuilder;
        private readonly IMarketingAiGenerator _aiGenerator;
        private readonly IMarketingFallbackBuilder _fallbackBuilder;

        public MarketingService(
            IDistributedCache cache,
            IMarketingContextBuilder contextBuilder,
            IMarketingAiGenerator aiGenerator,
            IMarketingFallbackBuilder fallbackBuilder)
        {
            _cache = cache;
            _contextBuilder = contextBuilder;
            _aiGenerator = aiGenerator;
            _fallbackBuilder = fallbackBuilder;
        }

        public async Task<MarketingPostsDto> GeneratePostsAsync(Product product, MarketingGenerationRequestDto? request = null)
        {
            request ??= new MarketingGenerationRequestDto();
            var cacheKey = BuildCacheKey(product.Id, request);

            try
            {
                var cached = await _cache.GetStringAsync(cacheKey);
                if (!string.IsNullOrWhiteSpace(cached))
                {
                    var cachedDto = JsonSerializer.Deserialize<MarketingPostsDto>(cached);
                    if (cachedDto != null)
                        return cachedDto;
                }
            }
            catch
            {
                // لو Redis واقع، كمل عادي بدون Cache
            }

            var context = _contextBuilder.Build(product);
            PlatformPostsDto platformPosts;
            try
            {
                platformPosts = await _aiGenerator.GenerateAsync(context, request);
            }
            catch
            {
                platformPosts = _fallbackBuilder.Build(context, request);
            }

            var posts = new MarketingPostsDto
            {
                ProductId = product.Id,
                ProductName = product.Name ?? string.Empty,
                Posts = platformPosts,
                GeneratedAt = DateTime.UtcNow
            };

            try
            {
                await _cache.SetStringAsync(
                    cacheKey,
                    JsonSerializer.Serialize(posts),
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
                    });
            }
            catch
            {
                // لو Redis واقع، تجاهل التخزين وكمل
            }

            return posts;
        }

        public async Task InvalidateCacheAsync(int productId)
        {
            try
            {
                await _cache.RemoveAsync($"marketing_posts_{productId}");
            }
            catch
            {
                // لو Redis واقع، تجاهل
            }
        }

        private static string BuildCacheKey(int productId, MarketingGenerationRequestDto request)
        {
            return $"marketing_posts_{productId}_{request.Audience}_{request.Tone}_{request.CampaignGoal}_{request.IncludeHashtags}_{request.Language}"
                .ToLowerInvariant()
                .Replace(" ", "_");
        }
    }
}