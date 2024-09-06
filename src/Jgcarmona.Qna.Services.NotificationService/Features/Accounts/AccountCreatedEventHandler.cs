using FluentEmail.Core;
using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Services.Common;
using Microsoft.Extensions.Options;
using Jgcarmona.Qna.Common.Configuration;

namespace Jgcarmona.Qna.Services.NotificationService.Features.Accounts
{
    public class AccountCreatedEventHandler : IEventHandler<AccountCreatedEvent>
    {
        private readonly IFluentEmail _emailSender;
        private readonly ILogger<AccountCreatedEventHandler> _logger;
        private readonly ApiSettings _apiSettings;

        public AccountCreatedEventHandler(IFluentEmail emailSender,
            ILogger<AccountCreatedEventHandler> logger,
            IOptions<ApiSettings> apiSettings)
        {
            _emailSender = emailSender;
            _logger = logger;
            _apiSettings = apiSettings.Value;
        }

        public async Task Handle(AccountCreatedEvent domainEvent)
        {
            try
            {
                var verificationLink = $"{_apiSettings.BaseUrl}/verify-email?token={domainEvent.VerificationToken.Id.ToString()}";

                var emailModel = new AccountCreatedEmailModel
                {
                    FirstName = domainEvent.Account.Profiles.FirstOrDefault()?.FirstName ?? "User",
                    VerificationLink = verificationLink
                };

                var templatePath = "/app/Features/Accounts/AccountCreatedTemplate.cshtml";

                var email = await _emailSender
                    .To(domainEvent.Account.Email)
                    .Subject("QnA - Email Verification")
                    .UsingTemplateFromFile(templatePath, emailModel)
                    .SendAsync();

                if (email.Successful)
                {
                    _logger.LogInformation("Email sent successfully to {Email}.", domainEvent.Account.Email);
                }
                else
                {
                    _logger.LogError("Failed to send email to {Email}.", domainEvent.Account.Email);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Email}.", domainEvent.Account.Email);
            }
        }
    }
}
