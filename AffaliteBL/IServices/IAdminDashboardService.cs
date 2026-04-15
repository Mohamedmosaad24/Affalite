using AffaliteBL.DTOs.Auth;
using AffaliteBL.DTOs.DashboardDTOs;

namespace AffaliteBL.IServices;

public interface IAdminDashboardService
{
    ApiResponseDTO<DashboardOverviewDTO> GetDashboardOverview();
    ApiResponseDTO<DashboardSummaryDTO> GetSummary();
    ApiResponseDTO<List<RecentOrderDTO>> GetRecentOrders(int count = 10);
    ApiResponseDTO<List<TopProductDTO>> GetTopProducts(int count = 5);
    ApiResponseDTO<List<TopAffiliateDTO>> GetTopAffiliates(int count = 5);
    ApiResponseDTO<RevenueChartDTO> GetRevenueChart();
}