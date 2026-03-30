namespace AffaliteBL.DTOs.OrderDTOs
{
	public class OrderCreateDTO
	{
		public int MerchantId { get; set; }
		public int? AffiliateId { get; set; }

		public string CustomerName { get; set; } = string.Empty;
		public string CustomerPhone { get; set; } = string.Empty;
		public string CustomerAddress { get; set; } = string.Empty;

		public decimal TotalPrice { get; set; }
		public decimal AffiliateCommissionPct { get; set; }

	}

}


