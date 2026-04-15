using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.DTOs.CartDTOs
{
    public class CartDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public int AffiliateId { get; set; }
        public decimal SubTotal { get; set; } //مجموع المنتجات
        public decimal Shiping { get; set; } = 10; //الشحن
        public decimal AffilaiteCommission { get; set; } //مجموع المنتجات
        public decimal Total { get; set; } // المنتجات + الشحن + العمولة
        public List<CartItemDTO> Items { get; set; } = new List<CartItemDTO>();
    }
}
