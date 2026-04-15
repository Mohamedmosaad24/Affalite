using AffaliteDAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace AffaliteBL.DTOs.NotificationDTOs;

public class CreateNotificationDTO
{
    [Required]
    public string UserId { get; set; }

    [Required]
    [MinLength(1)]
    public string Title { get; set; }

    [Required]
    [MinLength(1)]
    public string Message { get; set; }

    public NotificationType Type { get; set; }
    public string? RelatedEntityId { get; set; }
}

public class NotificationDTO
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public NotificationType Type { get; set; }
    public string TypeName => Type.ToString();
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? RelatedEntityId { get; set; }
}

public class NotificationQueryParams
{
    public bool? IsRead { get; set; }
    public NotificationType? Type { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}