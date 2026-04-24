using AffaliteBL.DTOs.AiDTOS;
using AffaliteBL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AffalitePL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MarketingController : ControllerBase
    {
        private readonly IMarketingService _marketingService;
        private readonly IProductService _productService;

        public MarketingController(IMarketingService marketingService, IProductService productService)
        {
            _marketingService = marketingService;
            _productService = productService;
        }

        [HttpGet("product/{productId:int}/posts")]
        public async Task<ActionResult<MarketingPostsDto>> GetMarketingPosts(
            int productId,
            [FromQuery] string? audience = null,
            [FromQuery] string? tone = null,
            [FromQuery] string? campaignGoal = null,
            [FromQuery] bool? includeHashtags = null,
            [FromQuery] string? language = null)
        {
            var product = _productService.GetById(productId);
            if (product is null)
                return NotFound(new { message = "المنتج مش موجود" });

            try
            {
                var options = new MarketingGenerationRequestDto
                {
                    Audience = audience ?? "عام",
                    Tone = tone ?? "مقنع",
                    CampaignGoal = campaignGoal ?? "زيادة المبيعات",
                    IncludeHashtags = includeHashtags ?? true,
                    Language = language ?? "ar"
                };

                var posts = await _marketingService.GeneratePostsAsync(product, options);
                return Ok(posts);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "AI service configuration is invalid.",
                    details = ex.Message
                });
            }
            catch (HttpRequestException ex)
            {
                var statusCode = ex.StatusCode == HttpStatusCode.Unauthorized || ex.StatusCode == HttpStatusCode.Forbidden
                    ? StatusCodes.Status502BadGateway
                    : StatusCodes.Status503ServiceUnavailable;

                return StatusCode(statusCode, new
                {
                    message = "Failed to generate AI marketing posts.",
                    details = ex.Message
                });
            }
        }

        [HttpPost("product/{productId:int}/posts/regenerate")]
        public async Task<ActionResult<MarketingPostsDto>> RegenerateMarketingPosts(
            int productId,
            [FromBody] MarketingGenerationRequestDto? request = null)
        {
            var product = _productService.GetById(productId);
            if (product is null)
                return NotFound(new { message = "المنتج مش موجود" });

            try
            {
                await _marketingService.InvalidateCacheAsync(productId);
                var posts = await _marketingService.GeneratePostsAsync(product, request);
                return Ok(posts);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "AI service configuration is invalid.",
                    details = ex.Message
                });
            }
            catch (HttpRequestException ex)
            {
                var statusCode = ex.StatusCode == HttpStatusCode.Unauthorized || ex.StatusCode == HttpStatusCode.Forbidden
                    ? StatusCodes.Status502BadGateway
                    : StatusCodes.Status503ServiceUnavailable;

                return StatusCode(statusCode, new
                {
                    message = "Failed to regenerate AI marketing posts.",
                    details = ex.Message
                });
            }
        }
    }
}