using AffaliteDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteDAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int? AffiliateId { get; set; }

        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerAddress { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }
        public decimal AffiliateCommissionPct { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //public List<Merchant> Merchants { get; set; }
        public ICollection<MerchantOrder> MerchantOrder { get; set; }
        public Affiliate? Affiliate { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public Commission? Commission { get; set; }
    }
}
