using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HordeFlow.HR.Infrastructure.Services.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            this.logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            this.logger.LogInformation($"{message}");
            return Task.CompletedTask;
        }
    }
}