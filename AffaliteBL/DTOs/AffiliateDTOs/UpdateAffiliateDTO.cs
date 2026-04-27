using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.DTOs.AffiliateDTOs
{
    public class UpdateAffiliateDTO
    {
        public decimal Balance { get; set; }
        public bool IsActive { get; set; } = true;
        public string FullName { get; set; }
        public string AppUserId { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
