using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.DTOs.MerchantCommissions
{
    public class MerchantCommissionsDTO
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public int CommissionId { get; set; }
        public decimal value { get; set; }
    }
}
