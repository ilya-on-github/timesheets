using System;
using MediatR;

namespace Timesheets.Services.Queries.Employees
{
    public class SingleEmployeeQuery : IRequest<IEmployee>
    {
        public Guid Id { get; }

        public SingleEmployeeQuery(Guid id)
        {
            Id = id;
        }
    }
}