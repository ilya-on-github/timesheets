using System;
using MediatR;

namespace Timesheets.Services.Queries.Accounts
{
    public class SingleAccountQuery : IRequest<IAccount>
    {
        public Guid Id { get; }

        public SingleAccountQuery(Guid id)
        {
            Id = id;
        }
    }
}