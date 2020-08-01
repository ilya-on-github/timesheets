using System;
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