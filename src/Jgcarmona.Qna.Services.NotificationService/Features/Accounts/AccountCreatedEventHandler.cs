using FluentEmail.Core;
using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Services.Common;
using Microsoft.Extensions.Options;
using Jgcarmona.Qna.Common.Configuration;

namespace Jgcarmona.Qna.Services.NotificationService.Features.Users
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
            var verificationLink = $"{_apiSettings.BaseUrl}/verify-email?token={domainEvent.VerificationToken.Id.ToString()}";

            await _emailSender
                .To(domainEvent.Account.Email)
                .Subject("Email Verification")
                .Body($"Please verify your email by clicking on this link: <a href='{verificationLink}'>Verify Email</a>", true)
                .SendAsync();
        }
    }
}
