using AffaliteBL.DTOs.MatchingDTOs;
using AffaliteBL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AffalitePL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MatchingController : ControllerBase
    {
        private readonly IMatchingService _matchingService;
        private readonly IAffiliateService _affiliateService;
        private readonly IMerchantService _merchantService;

        public MatchingController(
            IMatchingService matchingService,
            IAffiliateService affiliateService,
            IMerchantService merchantService)
        {
            _matchingService = matchingService;
            _affiliateService = affiliateService;
            _merchantService = merchantService;
        }

        [HttpGet("affiliates/{id}/recommendations")]
        public async Task<IActionResult> GetAffiliateRecommendations(int id)
        {
            var userId = User.FindFirst("uid")?.Value;
            var affiliate = _affiliateService.GetAffiliateUserId(userId);

            if (affiliate?.Id != id) return Forbid();

            var recommendations = await _matchingService.GetRecommendationsForAffiliateAsync(id);
            return Ok(recommendations);
        }

        [HttpGet("merchants/{id}/recommendations")]
        public async Task<IActionResult> GetMerchantRecommendations(int id)
        {
            var userId = User.FindFirst("uid")?.Value;
            var merchant = _merchantService.GetMerchantByUserId(userId);

            if (merchant?.Id != id) return Forbid();

            var recommendations = await _matchingService.GetRecommendationsForMerchantAsync(id);
            return Ok(recommendations);
        }

        [HttpPost("matches/{id}/respond")]
        public async Task<IActionResult> RespondToMatch(int id, [FromBody] MatchResponseRequest request)
        {
            var success = await _matchingService.ProcessMatchResponseAsync(id, request.UserId, request.IsAccepted);
            return Ok(new { success });
        }


        [HttpPost("run-job")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RunMatchingJob()
        {
            await _matchingService.RunMatchingJobAsync();
            return Ok(new { message = "Matching job completed successfully" });
        }
    }
}
