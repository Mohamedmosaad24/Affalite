using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.DTOs.MerchantDTOs
{
    public class GetMerchantDTO
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Email { get; set; }
        public int ProductsCount { get; set; }    
        public decimal TotalSales { get; set; }

    }
}
