using System;
using MediatR;

namespace Timesheets.Services.Commands.Worklogs
{
    public class CreateWorklogCommand : ICommand, IRequest<Guid>
    {
        public SetWorklogOptions Options { get; }

        public CreateWorklogCommand(SetWorklogOptions options)
        {
            Options = options;
        }
    }
}