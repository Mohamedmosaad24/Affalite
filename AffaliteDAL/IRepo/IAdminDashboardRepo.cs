namespace AffaliteDAL.IRepo;

public interface IAdminDashboardRepo
{
    int GetTotalOrders();
    decimal GetTotalRevenue();
    int GetTotalAffiliates();
    int GetTotalMerchants();
    int GetPendingCommissions();
    int GetActiveProducts();
    decimal GetTodayRevenue();
    int GetTodayOrders();
}