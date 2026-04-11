using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.DTOs.ReviewDTOs
{
    public class UpdateProductReviewDto
    {
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; } // 1 - 5
    }
}
