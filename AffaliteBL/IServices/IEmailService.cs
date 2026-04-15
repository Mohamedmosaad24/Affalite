namespace AffaliteBL.IServices;

public interface IEmailService
{
    Task<bool> SendEmailAsync(string to, string subject, string htmlBody);
    Task<bool> SendWelcomeEmailAsync(string email, string fullName, string role);
    Task<bool> SendOrderConfirmationEmailAsync(string email, string customerName, int orderId, decimal totalPrice);
}