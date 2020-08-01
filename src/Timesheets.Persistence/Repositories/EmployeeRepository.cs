using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Timesheets.Persistence.Models;
using Timesheets.Persistence.Queries;
using Timesheets.Services.Commands.Employees;

namespace Timesheets.Persistence.Repositories
{
    // ReSharper disable once UnusedType.Global
    public class EmployeeRepository : Repository, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<Employee> Get(Guid id, CancellationToken cancellationToken)
        {
            var dbEmployee = await DbContext.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(EmployeeSpecs.ById(id), cancellationToken);

            return Mapper.Map<Employee>(dbEmployee);
        }

        public async Task Save(Employee employee, CancellationToken cancellationToken)
        {
            var dbEmployee = Mapper.Map<DbEmployee>(employee);

            var existingEmployee = await DbContext.Employees
                .FirstOrDefaultAsync(EmployeeSpecs.ById(dbEmployee.Id), cancellationToken);

            if (existingEmployee == null)
            {
                await DbContext.Employees.AddAsync(dbEmployee, cancellationToken);
            }
            else
            {
                DbContext.Entry(existingEmployee).CurrentValues.SetValues(dbEmployee);
            }
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            var existingEmployee = await DbContext.Employees
                .FirstOrDefaultAsync(EmployeeSpecs.ById(id), cancellationToken);

            if (existingEmployee != null)
            {
                DbContext.Employees.Remove(existingEmployee);
            }
        }
    }
}