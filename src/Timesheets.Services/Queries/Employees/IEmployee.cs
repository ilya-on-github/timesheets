using System;

namespace Timesheets.Services.Queries.Employees
{
    public interface IEmployee
    {
        Guid Id { get; }
        string Name { get; }
    }
}