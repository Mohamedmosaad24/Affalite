using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AffaliteBLL.DTOs.Products;
using AffaliteDAL.Entities;

namespace AffaliteBL.DTOs.CartDTOs
{
    public class CartItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int Quantity { get; set; }
        public ProductDto? Product { get; set; }
        public decimal Price { get; set; }

    }
}
