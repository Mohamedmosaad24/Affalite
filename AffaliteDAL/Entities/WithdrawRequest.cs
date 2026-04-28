using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteDAL.Entities
{
    public class WithdrawRequest
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public decimal Amount { get; set; }

        public WithdrawalMethod PaymentMethod { get; set; } // Vodafone Cash / Bank / etc

        public UserType UserType { get; set; } // "Affiliate" or "Merchant"

        public int UserRefId { get; set; } // AffiliateId أو MerchantId

        public WithdrawalStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
    public enum UserType
    {
        Affiliate = 1,
        Merchant = 2
    }

    public enum WithdrawalMethod
    {
        VodafoneCash = 1,
        BankTransfer = 2,
        InstaPay = 3
    }

    public enum WithdrawalStatus
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3
    }
}
