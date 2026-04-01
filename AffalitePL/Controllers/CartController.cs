using AffaliteBL.DTOs.CartDTOs;
using AffaliteBL.IServices;
using AffaliteDAL.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace AffalitePL.Controllers
{
    //    ------------------------------------------------------------------
    //5) Cart(مهم جدًا)
    //GET     /api/cart/{cartId
    //}
    //POST / api / cart
    //DELETE / api / cart /{ cartId}
    

    //-------------------------------------------------------------------

    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }
        [HttpGet("{cartId}")]
        public IActionResult Get(int cartId)
        {
            var cart = _cartService.GetCartById(cartId);
            if (cart == null)
            {
                return NotFound($"Cart with id: {cartId} not found");
            }
            var result = _mapper.Map<Cart>(cart);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateCart()
        {
            var cart = _cartService.CreateCart();
            var result = _mapper.Map<Cart>(cart);
            return Ok(result);
        }
        [HttpDelete("{cartId}")]
        public IActionResult DeleteCart(int cartId)
        {
            var cart = _cartService.GetCartById(cartId);
            if (cart == null)
            {
                return NotFound($"Cart with id: {cartId} not found");
            }
            _cartService.DeleteCart(cartId);
            return Ok("Cart deleted successfully");
        }
        
        //POST / api / cart /{ cartId}/ items
        [HttpPost("{cartId}/items")]
        public IActionResult CreateItem(int cartId, AddCartItemDTO addCartItemDTO)
        {
            try
            {
                _cartService.CreateItem(cartId, addCartItemDTO);
                return Ok("Item added to cart successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //PUT / api / cart /{ cartId}/ items /{ itemId}
        [HttpPut("{cartId}/items/{itemId}")]
        public IActionResult UpdateItem(int cartId, int itemId, UpdateCartItemDTO updateCartItemDTO)
        {
            try
            {
                _cartService.UpdateItem(cartId, itemId, updateCartItemDTO);
                return Ok("Cart item updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //DELETE / api / cart /{ cartId}/ items /{ itemId}
        [HttpDelete("{cartId}/items/{itemId}")]
        public IActionResult DeleteItems(int cartId,int itemId)
        {
            try
            {
                _cartService.DeleteItem(cartId, itemId);
                return Ok("Cart item deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
