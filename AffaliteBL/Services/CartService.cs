using AffaliteBL.DTOs.CartDTOs;
using AffaliteBL.IServices;
using AffaliteDAL.Entities;
using AffaliteDAL.IRepo;

namespace AffaliteBL.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _repo;

        public CartService(ICartRepo repo)
        {
            _repo = repo;
        }

        public Cart? GetCartById(int id)
        {
            return _repo.GetCartWithItems(id);
        }

        public Cart CreateCart(int userId)
        {
            var cart = new Cart { AffiliateId= userId,CreatedAt=DateTime.Now };

            _repo.Add(cart);
            _repo.Save();

            return cart;
        }

        public void DeleteCart(int id)
        {
            var cart = _repo.GetById(id);
            if (cart == null)
                throw new Exception("Cart not found");

            _repo.Delete(cart);
            _repo.Save();
        }

        public void CreateItem(int cartId, AddCartItemDTO addCartItemDTO)
        {
            var cart = _repo.GetCartWithItems(cartId);
            if (cart == null)
                throw new Exception("Cart not found");

            var product = _repo.GetProduct(addCartItemDTO.ProductId);
            if (product == null)
                throw new Exception("Product not found");

            var existingItem = cart.Items?
                .FirstOrDefault(i => i.ProductId == addCartItemDTO.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += addCartItemDTO.Quantity;
                _repo.UpdateItem(existingItem);
            }
            else
            {
                var newItem = new CartItem
                {
                    CartId = cartId,
                    ProductId = addCartItemDTO.ProductId,
                    Quantity = addCartItemDTO.Quantity
                };

                _repo.AddItem(newItem);
            }

            _repo.Save();
        }

        public void UpdateItem(int cartId, int itemId, UpdateCartItemDTO updateCartItemDTO)
        {
            var item = _repo.GetCartItem(cartId, itemId);
            if (item == null)
                throw new Exception("Cart item not found");

            item.Quantity = updateCartItemDTO.Quantity;
            _repo.UpdateItem(item);
            _repo.Save();
        }

        public void DeleteItem(int cartId, int itemId)
        {
            var item = _repo.GetCartItem(cartId, itemId);
            if (item == null)
                throw new Exception("Cart item not found");

            _repo.DeleteItem(item);
            _repo.Save();
        }

        public void UpdateItemByProductId(int cartId, int productId, UpdateCartItemDTO updateCartItemDTO)
        {
            var cart = _repo.GetCartWithItems(cartId);
            if (cart == null)
                throw new Exception("Cart not found");

            var line = cart.Items?.FirstOrDefault(i => i.ProductId == productId);
            if (line == null)
                throw new Exception("Cart item not found");

            if (updateCartItemDTO.Quantity <= 0)
            {
                _repo.DeleteItem(line);
                _repo.Save();
                return;
            }

            line.Quantity = updateCartItemDTO.Quantity;
            _repo.UpdateItem(line);
            _repo.Save();
        }

        public void DeleteItemByProductId(int cartId, int productId)
        {
            var cart = _repo.GetCartWithItems(cartId);
            if (cart == null)
                throw new Exception("Cart not found");

            var line = cart.Items?.FirstOrDefault(i => i.ProductId == productId);
            if (line == null)
                throw new Exception("Cart item not found");

            _repo.DeleteItem(line);
            _repo.Save();
        }
    }
}