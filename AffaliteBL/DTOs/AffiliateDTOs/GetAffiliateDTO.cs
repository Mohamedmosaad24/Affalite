using AffaliteBL.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.DTOs.AffiliateDTOs
{
    public class GetAffiliateDTO 
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Email { get; set; }
        public string? AppUserId { get; set; }



    }
}
