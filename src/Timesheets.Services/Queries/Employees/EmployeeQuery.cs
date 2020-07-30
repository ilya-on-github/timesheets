using System;
using System.Collections.Generic;
using MediatR;

namespace Timesheets.Services.Queries.Employees
{
    public class EmployeeQuery : IRequest<IEnumerable<IEmployee>>
    {
        public EmployeeQuery(EmployeeFilter filter)
        {
            Filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }

        public EmployeeFilter Filter { get; }
    }
}