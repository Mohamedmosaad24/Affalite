using AffaliteBL.DTOs.CartDTOs;
using AffaliteBL.IServices;
using AffalitePL.Helpers;
using Mattger_BL.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AffalitePL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly string _imagesBaseUrl;

        public CartController(ICartService cartService, IOptions<ApiSettings> apiSettings)
        {
            _cartService = cartService;
            _imagesBaseUrl = apiSettings.Value.BaseUrl ?? string.Empty;
        }

        private CartUiResponseDto MapUi(int cartId)
        {
            var cart = _cartService.GetCartById(cartId);
            return CartUiMapper.Map(cart, _imagesBaseUrl);
        }

        [HttpGet("{cartId:int}")]
        public IActionResult Get(int cartId)
        {
            var cart = _cartService.GetCartById(cartId);
            if (cart == null)
                return NotFound($"Cart with id: {cartId} not found");

            return Ok(MapUi(cartId));
        }

        [HttpPost]
        public IActionResult CreateCart(int userId)
        {
            var cart = _cartService.CreateCart(userId);
            return Ok(CartUiMapper.Map(cart, _imagesBaseUrl));
        }

        [HttpDelete("{cartId:int}")]
        public IActionResult DeleteCart(int cartId)
        {
            var cart = _cartService.GetCartById(cartId);
            if (cart == null)
                return NotFound($"Cart with id: {cartId} not found");

            _cartService.DeleteCart(cartId);
            return Ok("Cart deleted successfully");
        }

        [HttpPost("{cartId:int}/items")]
        public IActionResult CreateItem(int cartId, [FromBody] AddCartItemDTO addCartItemDTO)
        {
            try
            {
                _cartService.CreateItem(cartId, addCartItemDTO);
                return Ok(MapUi(cartId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{cartId:int}/items/{itemId:int}")]
        public IActionResult UpdateItem(int cartId, int itemId, [FromBody] UpdateCartItemDTO updateCartItemDTO)
        {
            try
            {
                _cartService.UpdateItem(cartId, itemId, updateCartItemDTO);
                return Ok(MapUi(cartId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{cartId:int}/items/{itemId:int}")]
        public IActionResult DeleteItems(int cartId, int itemId)
        {
            try
            {
                _cartService.DeleteItem(cartId, itemId);
                return Ok(MapUi(cartId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>Matches Angular cart service (update by product id).</summary>
        [HttpPut("{cartId:int}/products/{productId:int}")]
        public IActionResult UpdateItemByProduct(int cartId, int productId, [FromBody] UpdateCartItemDTO updateCartItemDTO)
        {
            try
            {
                _cartService.UpdateItemByProductId(cartId, productId, updateCartItemDTO);
                return Ok(MapUi(cartId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>Matches Angular cart service (remove by product id).</summary>
        [HttpDelete("{cartId:int}/products/{productId:int}")]
        public IActionResult DeleteItemByProduct(int cartId, int productId)
        {
            try
            {
                _cartService.DeleteItemByProductId(cartId, productId);
                return Ok(MapUi(cartId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>Coupon handling not implemented yet; returns false for the SPA.</summary>
        [HttpPost("{cartId:int}/coupon")]
        public IActionResult ApplyCoupon(int cartId, [FromBody] ApplyCouponRequestDto? body)
        {
            _ = body?.Code;
            if (_cartService.GetCartById(cartId) == null)
                return NotFound($"Cart with id: {cartId} not found");

            return Ok(false);
        }
    }
}
