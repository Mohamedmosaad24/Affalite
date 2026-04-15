using AffaliteDAL.Entities;
using System.Linq;

namespace AffaliteDAL.IRepo;

public interface INotificationRepo : IGenericRepository<Notification>
{
    IEnumerable<Notification> GetByUserId(string userId);
    IEnumerable<Notification> GetUnreadByUserId(string userId);
    int GetUnreadCountByUserId(string userId);
    void MarkAsRead(int id);
    void MarkAllAsRead(string userId);
}