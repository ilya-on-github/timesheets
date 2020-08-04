using System;
using MediatR;

namespace Timesheets.Services.Commands.Worklogs
{
    public class UpdateWorklogCommand : IRequest
    {
        public Guid Id { get; }
        public SetWorklogOptions Options { get; }

        public UpdateWorklogCommand(Guid id, SetWorklogOptions options)
        {
            Id = id;
            Options = options;
        }
    }
}