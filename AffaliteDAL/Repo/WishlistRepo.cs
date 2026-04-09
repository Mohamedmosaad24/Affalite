using AffaliteBL.IServices;
using AffaliteDAL.Data;
using AffaliteDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteDAL.Repo;
public class WishlistRepo : IWishlistRepo
{
    private readonly AffaliteDBContext _context;

    public WishlistRepo(AffaliteDBContext context)
    {
        _context = context;
    }

    public List<Wishlist> GetByAffiliateId(int affiliateId)
    {
        return _context.Wishlists
            .Include(w => w.Product)
                .ThenInclude(p => p.Images)
            .Where(w => w.AffiliateId == affiliateId)
            .ToList();
    }

    public Wishlist? GetByAffiliateAndProduct(int affiliateId, int productId)
    {
        return _context.Wishlists
            .FirstOrDefault(w => w.AffiliateId == affiliateId
                              && w.ProductId == productId);
    }

    public void Add(Wishlist wishlist) => _context.Wishlists.Add(wishlist);
    public void Remove(Wishlist wishlist) => _context.Wishlists.Remove(wishlist);
    public void Save() => _context.SaveChanges();
}