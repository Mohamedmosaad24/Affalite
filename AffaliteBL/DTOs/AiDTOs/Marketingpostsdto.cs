namespace AffaliteBL.DTOs.AiDTOS
{
    public class MarketingPostsDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public PlatformPostsDto Posts { get; set; } = new();
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    }

    public class PlatformPostsDto
    {
        public string Facebook { get; set; } = string.Empty;
        public string Instagram { get; set; } = string.Empty;
        public string Twitter { get; set; } = string.Empty;
        public string LinkedIn { get; set; } = string.Empty;
    }
}