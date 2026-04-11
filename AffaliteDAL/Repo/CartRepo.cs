using AffaliteDAL.Data;
using AffaliteDAL.Entities;
using AffaliteDAL.IRepo;
using Microsoft.EntityFrameworkCore;

namespace AffaliteDAL.Repo
{
    public class CartRepo : GenericRepository<Cart>, ICartRepo
    {
        private readonly AffaliteDBContext _context;

        public CartRepo(AffaliteDBContext context) : base(context)
        {
            _context = context;
        }

        public Cart? GetCartWithItems(int cartId)
        {
            return _context.Carts

                .Include(c => c.Items)
                    .ThenInclude(i => i.Product)

                .Include(c => c.Items).ThenInclude(p=>p.Product).ThenInclude(p=>p.Images)

                .FirstOrDefault(c => c.Id == cartId);
        }
        public Cart? GetCartWithAffilaiteId(int uId)
        {
            return _context.Carts.Include(c => c.Items).ThenInclude(p => p.Product).ThenInclude(p => p.Images)
                .FirstOrDefault(c => c.AffiliateId == uId);
            
        }
        public CartItem? GetCartItem(int cartId, int itemId)
        {
            return _context.CartItems
                .FirstOrDefault(i => i.CartId == cartId && i.Id == itemId);
        }

        public Product? GetProduct(int productId)
        {
            return _context.Products
                .FirstOrDefault(p => p.Id == productId);
        }

        public void AddItem(CartItem item)
        {
            _context.CartItems.Add(item);
        }

        public void UpdateItem(CartItem item)
        {
            _context.CartItems.Update(item);
        }

        public void DeleteItem(CartItem item)
        {
            _context.CartItems.Remove(item);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}