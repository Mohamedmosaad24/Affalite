using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AffaliteDAL.Entities;

namespace AffaliteBL.DTOs.WithdrawalDto
{
    public class WithdrawRequestDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public decimal Amount { get; set; }

        public WithdrawalMethod PaymentMethod { get; set; } // Vodafone Cash / Bank / etc

        public UserType UserType { get; set; } // "Affiliate" or "Merchant"

        public int UserRefId { get; set; } // AffiliateId أو MerchantId

        public WithdrawalStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string UserName { get; set; }
    }
}
