using AffaliteDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.IServices;
public interface IWishlistRepo
{
    List<Wishlist> GetByAffiliateId(int affiliateId);
    Wishlist? GetByAffiliateAndProduct(int affiliateId, int productId);
    void Add(Wishlist wishlist);
    void Remove(Wishlist wishlist);
    void Save();
}
