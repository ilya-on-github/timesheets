using System;

namespace Timesheets.Services.Commands.Employees
{
    public class Employee
    {
        public Guid Id { get; }
        public string Name { get; private set; }

        public Employee(string name)
        {
            Id = Guid.NewGuid();

            SetName(name);
        }

        public Employee(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public EmployeeDescriptor Descriptor()
        {
            return new EmployeeDescriptor(Id, Name);
        }

        public void Update(string name)
        {
            SetName(name);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Employee name can't be empty.");
            }

            Name = name.Trim().RemoveDoubleSpaces();
        }
    }
}