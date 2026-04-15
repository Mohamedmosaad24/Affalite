namespace AffaliteBL.DTOs.CartDTOs
{
    public class CartUiItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }

    public class CartUiSummaryDto
    {
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal Total { get; set; }
        public int ItemCount { get; set; }
    }

    public class CartUiResponseDto
    {
        public int CartId { get; set; }
        public List<CartUiItemDto> Items { get; set; } = new();
        public CartUiSummaryDto Summary { get; set; } = new();
    }

    public class ApplyCouponRequestDto
    {
        public string? Code { get; set; }
    }
}
