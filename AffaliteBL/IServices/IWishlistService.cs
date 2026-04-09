using AffaliteBL.DTOs.WishlistDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.IServices;
public interface IWishlistService
{
    List<WishlistItemDTO> GetWishlist(int affiliateId);
    string AddToWishlist(int affiliateId, int productId);
    string RemoveFromWishlist(int affiliateId, int productId);
}
