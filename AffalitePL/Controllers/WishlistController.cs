using AffaliteBL.DTOs.WishlistDTOS;
using AffaliteBL.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AffalitePL.Controllers;
[Route("api/[controller]")]
[ApiController]
public class WishlistController : ControllerBase
{
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        // GET: /api/Wishlist
        [HttpGet]
        public IActionResult GetWishlist()
        {
            int affiliateId = 1; // هتجيبه من الـ JWT لاحقاً
            var result = _wishlistService.GetWishlist(affiliateId);
            return Ok(result);
        }

        // POST: /api/Wishlist
        [HttpPost]
        public IActionResult AddToWishlist([FromBody] AddToWishlistDTO dto)
        {
            int affiliateId = 1;
            var result = _wishlistService.AddToWishlist(affiliateId, dto.ProductId);
            return Ok(result);
        }

        // DELETE: /api/Wishlist/{productId}
        [HttpDelete("{productId}")]
        public IActionResult RemoveFromWishlist(int productId)
        {
            int affiliateId = 1;
            var result = _wishlistService.RemoveFromWishlist(affiliateId, productId);
            return Ok(result);
        }
    }
