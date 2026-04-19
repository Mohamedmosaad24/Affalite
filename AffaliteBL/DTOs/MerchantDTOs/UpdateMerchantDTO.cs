using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.DTOs.MerchantDTOs
{
    public class UpdateMerchantDTO
    {
        public decimal Balance { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; } = true;
        public string AppUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
