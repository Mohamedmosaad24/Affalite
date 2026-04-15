using AffaliteBL.IServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AffaliteBL.Services;

public class EmailSettings
{
    public string SmtpHost { get; set; } = "smtp.gmail.com";
    public int SmtpPort { get; set; } = 587;
    public string SmtpUser { get; set; } = "";
    public string SmtpPassword { get; set; } = "";
    public string FromEmail { get; set; } = "noreply@affalite.com";
    public string FromName { get; set; } = "Affalite";
    public bool EnableEmail { get; set; } = false;
}

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<EmailSettings> settings, ILogger<EmailService> logger)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<bool> SendEmailAsync(string to, string subject, string htmlBody)
    {
        try
        {
            if (!_settings.EnableEmail)
            {
                _logger.LogInformation("Email disabled. Would send to: {To}, Subject: {Subject}", to, subject);
                _logger.LogDebug("Email body: {Body}", htmlBody);
                return true;
            }

            using var client = new HttpClient();
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("to", to),
                new KeyValuePair<string, string>("subject", subject),
                new KeyValuePair<string, string>("body", htmlBody)
            });

            var response = await client.PostAsync($"http://localhost:5220/api/Email/send", content);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {To}", to);
            _logger.LogInformation("Email would be sent to: {To}, Subject: {Subject}", to, subject);
            return false;
        }
    }

    public async Task<bool> SendWelcomeEmailAsync(string email, string fullName, string role)
    {
        var subject = "Welcome to Affalite!";
        var htmlBody = $@"
            <html>
            <body style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;'>
                <div style='background-color: #f8f9fa; padding: 20px; border-radius: 10px;'>
                    <h1 style='color: #1a1a1a;'>Welcome, {fullName}!</h1>
                    <p style='color: #666;'>Thank you for registering as a <strong>{role}</strong> on Affalite.</p>
                    <p style='color: #666;'>We're excited to have you on board!</p>
                    <hr style='border: none; border-top: 1px solid #e0e0e0; margin: 20px 0;'>
                    <p style='color: #999; font-size: 12px;'>This is an automated message from Affalite.</p>
                </div>
            </body>
            </html>";

        return await SendEmailAsync(email, subject, htmlBody);
    }

    public async Task<bool> SendOrderConfirmationEmailAsync(string email, string customerName, int orderId, decimal totalPrice)
    {
        var subject = $"Order Confirmation - Order #{orderId}";
        var htmlBody = $@"
            <html>
            <body style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;'>
                <div style='background-color: #f8f9fa; padding: 20px; border-radius: 10px;'>
                    <h1 style='color: #2e7d32;'>Order Confirmed!</h1>
                    <p>Dear {customerName},</p>
                    <p>Your order has been successfully placed and is now <strong>confirmed</strong>.</p>
                    <div style='background-color: #e8f5e9; padding: 15px; border-radius: 8px; margin: 20px 0;'>
                        <p style='margin: 0;'><strong>Order ID:</strong> #{orderId}</p>
                        <p style='margin: 5px 0 0 0;'><strong>Total Amount:</strong> ${totalPrice:F2}</p>
                    </div>
                    <p>We'll notify you when your order ships.</p>
                    <hr style='border: none; border-top: 1px solid #e0e0e0; margin: 20px 0;'>
                    <p style='color: #999; font-size: 12px;'>Thank you for shopping with Affalite!</p>
                </div>
            </body>
            </html>";

        return await SendEmailAsync(email, subject, htmlBody);
    }
}