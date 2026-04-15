namespace AffaliteDAL.Entities;

public enum NotificationType
{
    System = 0,
    Order = 1,
    Payment = 2,
    Commission = 3,
    Affiliate = 4,
    Merchant = 5
}

public class Notification
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public NotificationType Type { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? RelatedEntityId { get; set; }

    public AppUser? User { get; set; }
}