namespace AffaliteBL.DTOs.DashboardDTOs;

public class DashboardSummaryDTO
{
    public int TotalOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public int TotalAffiliates { get; set; }
    public int TotalMerchants { get; set; }
    public int PendingCommissions { get; set; }
    public int ActiveProducts { get; set; }
    public decimal TodayRevenue { get; set; }
    public int TodayOrders { get; set; }
}

public class RecentOrderDTO
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string? AffiliateName { get; set; }
}

public class TopProductDTO
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int OrderCount { get; set; }
    public decimal TotalSales { get; set; }
    public string? ImageUrl { get; set; }
}

public class TopAffiliateDTO
{
    public string AffiliateId { get; set; } = string.Empty;
    public string AffiliateName { get; set; } = string.Empty;
    public int TotalOrders { get; set; }
    public decimal TotalEarnings { get; set; }
}

public class RevenueChartDTO
{
    public List<RevenueDataPoint> Daily { get; set; } = new();
    public List<RevenueDataPoint> Weekly { get; set; } = new();
    public List<RevenueDataPoint> Monthly { get; set; } = new();
}

public class RevenueDataPoint
{
    public string Label { get; set; } = string.Empty;
    public decimal Revenue { get; set; }
    public int Orders { get; set; }
}

public class DashboardOverviewDTO
{
    public DashboardSummaryDTO Summary { get; set; } = new();
    public List<RecentOrderDTO> RecentOrders { get; set; } = new();
    public List<TopProductDTO> TopProducts { get; set; } = new();
    public List<TopAffiliateDTO> TopAffiliates { get; set; } = new();
    public RevenueChartDTO RevenueChart { get; set; } = new();
}