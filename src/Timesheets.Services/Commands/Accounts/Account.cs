using System;
using Timesheets.Services.Commands.Issues;
using Timesheets.Services.Queries.Accounts;

namespace Timesheets.Services.Commands.Accounts
{
    public class Account : IAccount
    {
        public Guid Id { get; }
        public string Name { get; private set; }

        public Account(string name)
        {
            Id = Guid.NewGuid();
            SetName(name);
        }

        public Account(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public AccountDescriptor Descriptor()
        {
            return new AccountDescriptor(Id, Name);
        }

        public void Update(string name)
        {
            SetName(name);
        }

        private void SetName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Account name can't be empty.", nameof(input));
            }

            Name = input.Trim().RemoveDoubleSpaces();
        }
    }
}