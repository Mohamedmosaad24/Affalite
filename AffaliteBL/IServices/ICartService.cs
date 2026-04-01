using AffaliteBL.DTOs.CartDTOs;
using AffaliteDAL.Entities;

namespace AffaliteBL.IServices
{
    public interface ICartService
    {
        Cart? GetCartById(int id);
        Cart CreateCart();
        void DeleteCart(int id);

        void CreateItem(int cartId, AddCartItemDTO addCartItemDTO);
        void UpdateItem(int cartId, int itemId, UpdateCartItemDTO updateCartItemDTO);
        void DeleteItem(int cartId, int itemId);
    }
}