using AffaliteDAL.Entities;

namespace AffaliteDAL.IRepo
{
    public interface ICartRepo : IGenericRepository<Cart>
    {
        Cart? GetCartWithItems(int cartId);
        CartItem? GetCartItem(int cartId, int itemId);
        Product? GetProduct(int productId);

        void AddItem(CartItem item);
        void UpdateItem(CartItem item);
        void DeleteItem(CartItem item);

        void Save();
    }
}