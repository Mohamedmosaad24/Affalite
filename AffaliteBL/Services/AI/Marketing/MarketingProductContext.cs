namespace AffaliteBL.Services.AI.Marketing
{
    public class MarketingProductContext
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int SaleCount { get; set; }
        public int ReviewsCount { get; set; }
        public decimal AverageRating { get; set; }
        public List<string> TopReviewHighlights { get; set; } = new();
    }
}
