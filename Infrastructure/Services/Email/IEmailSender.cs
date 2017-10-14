using System.Threading.Tasks;

namespace HordeFlow.HR.Infrastructure.Services.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}