using AffaliteBL.DTOs.Auth;
using AffaliteBL.DTOs.NotificationDTOs;
using AffaliteBL.IServices;
using AffaliteDAL.Entities;
using AffaliteDAL.IRepo;
using System.Linq;

namespace AffaliteBL.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepo _notificationRepo;

    public NotificationService(INotificationRepo notificationRepo)
    {
        _notificationRepo = notificationRepo;
    }

    public ApiResponseDTO<NotificationDTO> CreateNotification(CreateNotificationDTO model)
    {
        try
        {
            var notification = new Notification
            {
                UserId = model.UserId,
                Title = model.Title,
                Message = model.Message,
                Type = model.Type,
                RelatedEntityId = model.RelatedEntityId,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            _notificationRepo.Add(notification);
            _notificationRepo.SaveChanges();

            var dto = MapToDTO(notification);
            return new ApiResponseDTO<NotificationDTO>
            {
                Success = true,
                Message = "Notification created successfully",
                Data = dto
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDTO<NotificationDTO>
            {
                Success = false,
                Message = "Failed to create notification",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public ApiResponseDTO<NotificationDTO> GetNotificationById(int id, string userId)
    {
        var notifications = _notificationRepo.GetByUserId(userId);
        var notification = notifications.FirstOrDefault(n => n.Id == id);

        if (notification == null)
        {
            return new ApiResponseDTO<NotificationDTO>
            {
                Success = false,
                Message = "Notification not found",
                Errors = new List<string> { "Notification not found or access denied" }
            };
        }

        return new ApiResponseDTO<NotificationDTO>
        {
            Success = true,
            Message = "Notification retrieved successfully",
            Data = MapToDTO(notification)
        };
    }

    public ApiResponseDTO<List<NotificationDTO>> GetUserNotifications(string userId, NotificationQueryParams queryParams)
    {
        try
        {
            IEnumerable<Notification> notifications;

            if (queryParams.IsRead.HasValue)
            {
                notifications = queryParams.IsRead.Value
                    ? _notificationRepo.GetAll().Where(n => n.UserId == userId && n.IsRead)
                    : _notificationRepo.GetUnreadByUserId(userId);
            }
            else
            {
                notifications = _notificationRepo.GetByUserId(userId);
            }

            if (queryParams.Type.HasValue)
            {
                notifications = notifications.Where(n => n.Type == queryParams.Type.Value);
            }

            var paginated = notifications
                .Skip((queryParams.Page - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToList();

            var dtos = paginated.Select(MapToDTO).ToList();

            return new ApiResponseDTO<List<NotificationDTO>>
            {
                Success = true,
                Message = "Notifications retrieved successfully",
                Data = dtos
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDTO<List<NotificationDTO>>
            {
                Success = false,
                Message = "Failed to retrieve notifications",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public ApiResponseDTO<int> GetUnreadCount(string userId)
    {
        try
        {
            var count = _notificationRepo.GetUnreadCountByUserId(userId);
            return new ApiResponseDTO<int>
            {
                Success = true,
                Message = "Unread count retrieved",
                Data = count
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDTO<int>
            {
                Success = false,
                Message = "Failed to get unread count",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public ApiResponseDTO<object> MarkAsRead(int id, string userId)
    {
        try
        {
            var notifications = _notificationRepo.GetByUserId(userId);
            var notification = notifications.FirstOrDefault(n => n.Id == id);

            if (notification == null)
            {
                return new ApiResponseDTO<object>
                {
                    Success = false,
                    Message = "Notification not found",
                    Errors = new List<string> { "Notification not found or access denied" }
                };
            }

            _notificationRepo.MarkAsRead(id);
            return new ApiResponseDTO<object>
            {
                Success = true,
                Message = "Notification marked as read"
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDTO<object>
            {
                Success = false,
                Message = "Failed to mark notification as read",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public ApiResponseDTO<object> MarkAllAsRead(string userId)
    {
        try
        {
            _notificationRepo.MarkAllAsRead(userId);
            return new ApiResponseDTO<object>
            {
                Success = true,
                Message = "All notifications marked as read"
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDTO<object>
            {
                Success = false,
                Message = "Failed to mark all notifications as read",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public ApiResponseDTO<object> DeleteNotification(int id, string userId)
    {
        try
        {
            var notifications = _notificationRepo.GetByUserId(userId);
            var notification = notifications.FirstOrDefault(n => n.Id == id);

            if (notification == null)
            {
                return new ApiResponseDTO<object>
                {
                    Success = false,
                    Message = "Notification not found",
                    Errors = new List<string> { "Notification not found or access denied" }
                };
            }

            _notificationRepo.Delete(notification);
            _notificationRepo.SaveChanges();

            return new ApiResponseDTO<object>
            {
                Success = true,
                Message = "Notification deleted successfully"
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDTO<object>
            {
                Success = false,
                Message = "Failed to delete notification",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    private static NotificationDTO MapToDTO(Notification notification)
    {
        return new NotificationDTO
        {
            Id = notification.Id,
            UserId = notification.UserId,
            Title = notification.Title,
            Message = notification.Message,
            Type = notification.Type,
            IsRead = notification.IsRead,
            CreatedAt = notification.CreatedAt,
            RelatedEntityId = notification.RelatedEntityId
        };
    }
}