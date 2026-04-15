using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.DTOs.ReviewDTOs
{
        public class ProductReviewDto
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public int AffiliateName { get; set; }
            public string Comment { get; set; } = string.Empty;
            public int Rating { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    
}

