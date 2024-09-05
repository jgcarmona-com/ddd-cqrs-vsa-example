using FluentEmail.Core;
using Jgcarmona.Qna.Domain.Events;

namespace Jgcarmona.Qna.Services.NotificationService.Features.Users
{
    public class AccountCreatedEventHandler : IEventHandler<AccountCreatedEvent>
    {
        private readonly IFluentEmail _emailSender;

        private readonly ILogger<AccountCreatedEventHandler> _logger;

        public AccountCreatedEventHandler(IFluentEmail emailSender,
            ILogger<AccountCreatedEventHandler> logger)
        {
            _emailSender = emailSender;
            _logger = logger;
        }

        public async Task Handle(AccountCreatedEvent domainEvent)
        {
            var verificationLink = $"https://yourapp.com/verify-email?token={domainEvent.VerificationToken}";

            await _emailSender
                .To(domainEvent.Account.Email)
                .Subject("Email Verification")
                .Body($"Please verify your email by clicking on this link: <a href='{verificationLink}'>Verify Email</a>")
                .SendAsync();
        }
    }
}
