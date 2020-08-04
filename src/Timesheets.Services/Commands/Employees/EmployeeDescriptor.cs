using System;

namespace Timesheets.Services.Commands.Employees
{
    public class EmployeeDescriptor
    {
        public Guid Id { get; }
        public string Name { get; }

        public EmployeeDescriptor(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}