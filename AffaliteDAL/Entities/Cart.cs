using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteDAL.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public decimal SubTotal { get; set; } = 0;//مجموع المنتجات
        public decimal Shiping { get; set; } = 10; //الشحن
        public decimal AffilaiteCommission { get; set; } = 0; //مجموع المنتجات
        public decimal Total { get; set; } = 0;// المنتجات + الشحن + العمولة
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
        public int AffiliateId { get; set; }
        public Affiliate Affiliate { get; set; }
    }
}
