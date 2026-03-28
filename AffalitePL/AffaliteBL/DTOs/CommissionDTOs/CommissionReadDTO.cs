using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.DTOs.CommissionDTOs
{
    public class CommissionReadDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal AffiliateAmount { get; set; }
        public decimal PlatformAmount { get; set; }
        public decimal MerchantAmount { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
