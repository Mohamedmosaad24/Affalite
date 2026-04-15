using AffaliteBL.DTOs.Auth;
using AffaliteBL.DTOs.DashboardDTOs;
using AffaliteBL.IServices;
using AffaliteDAL.Data;
using AffaliteDAL.Entities.Enums;
using AffaliteDAL.IRepo;
using Microsoft.EntityFrameworkCore;

namespace AffaliteBL.Services;

public class AdminDashboardService : IAdminDashboardService
{
    private readonly IAdminDashboardRepo _dashboardRepo;
    private readonly AffaliteDBContext _context;

    public AdminDashboardService(IAdminDashboardRepo dashboardRepo, AffaliteDBContext context)
    {
        _dashboardRepo = dashboardRepo;
        _context = context;
    }

    public ApiResponseDTO<DashboardOverviewDTO> GetDashboardOverview()
    {
        try
        {
            var overview = new DashboardOverviewDTO
            {
                Summary = GetSummary().Data ?? new DashboardSummaryDTO(),
                RecentOrders = GetRecentOrders().Data ?? new List<RecentOrderDTO>(),
                TopProducts = GetTopProducts().Data ?? new List<TopProductDTO>(),
                TopAffiliates = GetTopAffiliates().Data ?? new List<TopAffiliateDTO>(),
                RevenueChart = GetRevenueChart().Data ?? new RevenueChartDTO()
            };

            return new ApiResponseDTO<DashboardOverviewDTO>
            {
                Success = true,
                Message = "Dashboard overview retrieved successfully",
                Data = overview
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDTO<DashboardOverviewDTO>
            {
                Success = false,
                Message = "Failed to retrieve dashboard overview",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public ApiResponseDTO<DashboardSummaryDTO> GetSummary()
    {
        try
        {
            var summary = new DashboardSummaryDTO
            {
                TotalOrders = _dashboardRepo.GetTotalOrders(),
                TotalRevenue = _dashboardRepo.GetTotalRevenue(),
                TotalAffiliates = _dashboardRepo.GetTotalAffiliates(),
                TotalMerchants = _dashboardRepo.GetTotalMerchants(),
                PendingCommissions = _dashboardRepo.GetPendingCommissions(),
                ActiveProducts = _dashboardRepo.GetActiveProducts(),
                TodayRevenue = _dashboardRepo.GetTodayRevenue(),
                TodayOrders = _dashboardRepo.GetTodayOrders()
            };

            return new ApiResponseDTO<DashboardSummaryDTO>
            {
                Success = true,
                Message = "Summary retrieved successfully",
                Data = summary
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDTO<DashboardSummaryDTO>
            {
                Success = false,
                Message = "Failed to retrieve summary",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public ApiResponseDTO<List<RecentOrderDTO>> GetRecentOrders(int count = 10)
    {
        try
        {
            var orders = _context.Orders
                .Include(o => o.Affiliate)
                .ThenInclude(a => a!.AppUser)
                .OrderByDescending(o => o.CreatedAt)
                .Take(count)
                .Select(o => new RecentOrderDTO
                {
                    Id = o.Id,
                    CustomerName = o.CustomerName,
                    TotalPrice = o.TotalPrice,
                    Status = o.Status.ToString(),
                    CreatedAt = o.CreatedAt,
                    AffiliateName = o.Affiliate != null ? o.Affiliate.AppUser.FullName : null
                })
                .ToList();

            return new ApiResponseDTO<List<RecentOrderDTO>>
            {
                Success = true,
                Message = "Recent orders retrieved successfully",
                Data = orders
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDTO<List<RecentOrderDTO>>
            {
                Success = false,
                Message = "Failed to retrieve recent orders",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public ApiResponseDTO<List<TopProductDTO>> GetTopProducts(int count = 5)
    {
        try
        {
            var topProducts = _context.OrderItems
                .Include(oi => oi.Product)
                .GroupBy(oi => new { oi.ProductId, oi.Product.Name, oi.Product.Images })
                .Select(g => new TopProductDTO
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.Name,
                    OrderCount = g.Sum(oi => oi.Quantity),
                    TotalSales = g.Sum(oi => oi.Price * oi.Quantity),
                    //ImageUrl = g.Key.Images.FirstOrDefault()
                })
                .OrderByDescending(p => p.TotalSales)
                .Take(count)
                .ToList();

            return new ApiResponseDTO<List<TopProductDTO>>
            {
                Success = true,
                Message = "Top products retrieved successfully",
                Data = topProducts
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDTO<List<TopProductDTO>>
            {
                Success = false,
                Message = "Failed to retrieve top products",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public ApiResponseDTO<List<TopAffiliateDTO>> GetTopAffiliates(int count = 5)
    {
        try
        {
            var topAffiliates = _context.Commissions
                .Include(c => c.Order)
                .ThenInclude(o => o.Affiliate)
                .ThenInclude(a => a!.AppUser)
                .Where(c => c.Status == CommissionStatus.Paid)
                .GroupBy(c => new
                {
                    c.Order!.AffiliateId,
                    AffiliateName = c.Order.Affiliate != null ? c.Order.Affiliate.AppUser.FullName : "Unknown"
                })
                .Select(g => new TopAffiliateDTO
                {
                    AffiliateId = g.Key.AffiliateId.ToString(),
                    AffiliateName = g.Key.AffiliateName,
                    TotalOrders = g.Count(),
                    TotalEarnings = g.Sum(c => c.AffiliateAmount)
                })
                .OrderByDescending(a => a.TotalEarnings)
                .Take(count)
                .ToList();

            return new ApiResponseDTO<List<TopAffiliateDTO>>
            {
                Success = true,
                Message = "Top affiliates retrieved successfully",
                Data = topAffiliates
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDTO<List<TopAffiliateDTO>>
            {
                Success = false,
                Message = "Failed to retrieve top affiliates",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public ApiResponseDTO<RevenueChartDTO> GetRevenueChart()
    {
        try
        {
            var today = DateTime.UtcNow.Date;
            var last30Days = Enumerable.Range(0, 30)
                .Select(i => today.AddDays(-29 + i))
                .ToList();

            var last12Weeks = Enumerable.Range(0, 12)
                .Select(i => today.AddDays(-11 * 7 + i * 7))
                .ToList();

            var last12Months = Enumerable.Range(0, 12)
                .Select(i => new DateTime(today.Year, today.Month, 1).AddMonths(-11 + i))
                .ToList();

            var dailyData = last30Days
                .Select(date => new RevenueDataPoint
                {
                    Label = date.ToString("MMM dd"),
                    Revenue = _context.Orders
                        .Where(o => o.CreatedAt.Date == date &&
                                   (o.Status == OrderStatus.Delivered || o.Status == OrderStatus.Shipped))
                        .Sum(o => o.TotalPrice),
                    Orders = _context.Orders.Count(o => o.CreatedAt.Date == date)
                })
                .ToList();

            var weeklyData = last12Weeks
                .Select(weekStart => new RevenueDataPoint
                {
                    Label = $"Week of {weekStart:MMM dd}",
                    Revenue = _context.Orders
                        .Where(o => o.CreatedAt >= weekStart && o.CreatedAt < weekStart.AddDays(7) &&
                                   (o.Status == OrderStatus.Delivered || o.Status == OrderStatus.Shipped))
                        .Sum(o => o.TotalPrice),
                    Orders = _context.Orders.Count(o => o.CreatedAt >= weekStart && o.CreatedAt < weekStart.AddDays(7))
                })
                .ToList();

            var monthlyData = last12Months
                .Select(monthStart => new RevenueDataPoint
                {
                    Label = monthStart.ToString("MMM yyyy"),
                    Revenue = _context.Orders
                        .Where(o => o.CreatedAt.Year == monthStart.Year &&
                                   o.CreatedAt.Month == monthStart.Month &&
                                   (o.Status == OrderStatus.Delivered || o.Status == OrderStatus.Shipped))
                        .Sum(o => o.TotalPrice),
                    Orders = _context.Orders.Count(o => o.CreatedAt.Year == monthStart.Year && o.CreatedAt.Month == monthStart.Month)
                })
                .ToList();

            var chartData = new RevenueChartDTO
            {
                Daily = dailyData,
                Weekly = weeklyData,
                Monthly = monthlyData
            };

            return new ApiResponseDTO<RevenueChartDTO>
            {
                Success = true,
                Message = "Revenue chart retrieved successfully",
                Data = chartData
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDTO<RevenueChartDTO>
            {
                Success = false,
                Message = "Failed to retrieve revenue chart",
                Errors = new List<string> { ex.Message }
            };
        }
    }
}