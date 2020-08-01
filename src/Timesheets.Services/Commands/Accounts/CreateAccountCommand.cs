using MediatR;
using Timesheets.Services.Queries.Accounts;

namespace Timesheets.Services.Commands.Accounts
{
    public class CreateAccountCommand : ICommand, IRequest<IAccount>
    {
        public string Name { get; }

        public CreateAccountCommand(string name)
        {
            Name = name;
        }
    }
}