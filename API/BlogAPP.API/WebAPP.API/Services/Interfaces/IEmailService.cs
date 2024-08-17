using MimeKit;
using WebAPP.API.Models.ServiceModels;

namespace WebAPP.API.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
        void SendEmail(Message message);
    }
}
