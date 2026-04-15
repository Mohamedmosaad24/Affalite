using AffaliteBL.DTOs.CartDTOs;
using AffaliteDAL.Entities;

namespace AffaliteBL.IServices
{
    public interface ICartService
    {
        CartDTO? GetCartByUserId(int userId);
        //Cart CreateCart(int userId);
        void DeleteCart(int id);

        void CreateItem(int cartId, AddCartItemDTO? addCartItemDTO, decimal? affilaiteCommission);
        //void UpdateItem(int cartId, int itemId, UpdateCartItemDTO updateCartItemDTO);
        //void DeleteItem(int cartId, int itemId);
        //void UpdateItemByProductId(int cartId, int productId, UpdateCartItemDTO updateCartItemDTO);
        void DeleteItemByProductId(int userId, int productId);
    }
}