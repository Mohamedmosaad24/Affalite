using AffaliteDAL.Data;
using AffaliteDAL.Entities;
using AffaliteDAL.IRepo;
using Microsoft.EntityFrameworkCore;

namespace AffaliteDAL.Repo;

public class NotificationRepo : GenericRepository<Notification>, INotificationRepo
{
    public NotificationRepo(AffaliteDBContext context) : base(context)
    {

    }

    public IEnumerable<Notification> GetByUserId(string userId)
    {
        return _context.Notifications
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .ToList();
    }

    public IEnumerable<Notification> GetUnreadByUserId(string userId)
    {
        return _context.Notifications
            .Where(n => n.UserId == userId && !n.IsRead)
            .OrderByDescending(n => n.CreatedAt)
            .ToList();
    }

    public int GetUnreadCountByUserId(string userId)
    {
        return _context.Notifications
            .Count(n => n.UserId == userId && !n.IsRead);
    }

    public void MarkAsRead(int id)
    {
        var notification = _context.Notifications.Find(id);
        if (notification != null)
        {
            notification.IsRead = true;
            _context.SaveChanges();
        }
    }

    public void MarkAllAsRead(string userId)
    {
        var notifications = _context.Notifications
            .Where(n => n.UserId == userId && !n.IsRead)
            .ToList();

        foreach (var notification in notifications)
        {
            notification.IsRead = true;
        }
        _context.SaveChanges();
    }
}