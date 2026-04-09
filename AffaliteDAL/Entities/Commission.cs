using AffaliteDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteDAL.Entities
{
    public class Commission
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal AffiliateAmount { get; set; }
        public decimal PlatformAmount { get; set; }
        public decimal MerchantAmount { get; set; }
        public CommissionStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Order? Order { get; set; }
        public List<MerchantCommissions> MerchantCommissions { get; set; }
    }
  
}
