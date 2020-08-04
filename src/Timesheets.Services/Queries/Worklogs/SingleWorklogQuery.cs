using System;
using MediatR;

namespace Timesheets.Services.Queries.Worklogs
{
    public class SingleWorklogQuery : IRequest<IWorklog>
    {
        public Guid Id { get; }

        public SingleWorklogQuery(Guid id)
        {
            Id = id;
        }
    }
}