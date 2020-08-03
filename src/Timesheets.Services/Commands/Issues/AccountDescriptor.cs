using System;

namespace Timesheets.Services.Commands.Issues
{
    public class AccountDescriptor
    {
        public Guid Id { get; }
        public string Name { get; }

        public AccountDescriptor(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}