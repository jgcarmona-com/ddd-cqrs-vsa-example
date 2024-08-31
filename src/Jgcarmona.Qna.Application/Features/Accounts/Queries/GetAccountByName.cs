using Jgcarmona.Qna.Application.Features.Admin.Models;
using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Repositories.Command;
using Jgcarmona.Qna.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Jgcarmona.Qna.Application.Features.Accounts.Queries
{
    public class GetAccountByNameQuery : IRequest<AccountModel>
    {
        public string Username { get; set; }

        public GetAccountByNameQuery(string username)
        {
            Username = username;
        }
    }

    public class GetAccountByNameQueryHandler : IRequestHandler<GetAccountByNameQuery, AccountModel>
    {
        private readonly IAccountCommandRepository _accountRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<GetAccountByNameQueryHandler> _logger;

        public GetAccountByNameQueryHandler(
            IAccountCommandRepository accountRepository,
            ILogger<GetAccountByNameQueryHandler> logger,
            IEventDispatcher eventDispatcher,
            IHttpContextAccessor httpContextAccessor)
        {
            _accountRepository = accountRepository;
            _eventDispatcher = eventDispatcher;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<AccountModel> Handle(GetAccountByNameQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByNameAsync(request.Username);
            if (account == null)
            {
                return null;
            }

            var correlationId = _httpContextAccessor.HttpContext?.Items["CorrelationId"]?.ToString() ?? string.Empty;

            // Dispatch the event
            var accountViewedEvent = new AccountViewedEvent(account.Id, account.LoginName)
            {
                CorrelationId = correlationId
            };

            await _eventDispatcher.DispatchAsync(accountViewedEvent);

            return AccountModel.FromEntity(account);
        }
    }
}