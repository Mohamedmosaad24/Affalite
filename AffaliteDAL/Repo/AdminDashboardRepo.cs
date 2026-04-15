using AffaliteDAL.Data;
using AffaliteDAL.Entities.Enums;
using AffaliteDAL.IRepo;
using Microsoft.EntityFrameworkCore;

namespace AffaliteDAL.Repo;

public class AdminDashboardRepo : IAdminDashboardRepo
{
    private readonly AffaliteDBContext _context;

    public AdminDashboardRepo(AffaliteDBContext context)
    {
        _context = context;
    }

    public int GetTotalOrders()
    {
        return _context.Orders.Count();
    }

    public decimal GetTotalRevenue()
    {
        return _context.Orders
            .Where(o => o.Status == OrderStatus.Delivered || o.Status == OrderStatus.Shipped)
            .Sum(o => o.TotalPrice);
    }

    public int GetTotalAffiliates()
    {
        return _context.Affiliates.Count();
    }

    public int GetTotalMerchants()
    {
        return _context.Merchants.Count();
    }

    public int GetPendingCommissions()
    {
        return _context.Commissions
            .Count(c => c.Status == CommissionStatus.Pending);
    }

    public int GetActiveProducts()
    {
        return _context.Products
            .Count(p => p.Status == ProductStatus.Active);
    }

    public decimal GetTodayRevenue()
    {
        var today = DateTime.UtcNow.Date;
        return _context.Orders
            .Where(o => o.CreatedAt.Date == today &&
                       (o.Status == OrderStatus.Delivered || o.Status == OrderStatus.Shipped))
            .Sum(o => o.TotalPrice);
    }

    public int GetTodayOrders()
    {
        var today = DateTime.UtcNow.Date;
        return _context.Orders
            .Count(o => o.CreatedAt.Date == today);
    }
}