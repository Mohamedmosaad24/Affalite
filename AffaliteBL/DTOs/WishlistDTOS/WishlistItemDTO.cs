using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.DTOs.WishlistDTOS;
public  class WishlistItemDTO
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public double? Rating { get; set; }
    public DateTime CreatedAt { get; set; }
}
