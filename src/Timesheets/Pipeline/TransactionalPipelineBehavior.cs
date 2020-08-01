using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timesheets.Persistence;
using Timesheets.Services.Commands;
using Timesheets.Services.Commands.Accounts;

namespace Timesheets.Pipeline
{
    public class TransactionalPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly AppDbContext _appDbContext;

        public TransactionalPipelineBehavior(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (request is ICommand)
            {
                return await HandleCommand(cancellationToken, next);
            }

            return await next();
        }

        private async Task<TResponse> HandleCommand(CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            await using var transaction = await _appDbContext.Database.BeginTransactionAsync(cancellationToken);

            var response = await next();

            await _appDbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return response;
        }
    }
}