using AffaliteBL.DTOs.Auth;
using AffaliteBL.DTOs.NotificationDTOs;

namespace AffaliteBL.IServices;

public interface INotificationService
{
    ApiResponseDTO<NotificationDTO> CreateNotification(CreateNotificationDTO model);
    ApiResponseDTO<List<NotificationDTO>> GetUserNotifications(string userId, NotificationQueryParams queryParams);
    ApiResponseDTO<int> GetUnreadCount(string userId);
    ApiResponseDTO<object> MarkAsRead(int id, string userId);
    ApiResponseDTO<object> MarkAllAsRead(string userId);
    ApiResponseDTO<object> DeleteNotification(int id, string userId);

}