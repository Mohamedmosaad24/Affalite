namespace AffaliteBL.DTOs.OrderDTOs
{
    public class OrderReadDTO
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal AffiliateCommissionPct { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string AffiliateName { get; set; }  // ← أضف ده

        public List<OrderItemDTO> Items { get; set; } = new();
    }
}