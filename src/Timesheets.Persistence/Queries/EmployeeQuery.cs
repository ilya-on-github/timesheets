using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Timesheets.Services.Queries.Employees;

namespace Timesheets.Persistence.Queries
{
    public class EmployeeQuery : Query, IEmployeeQuery
    {
        public EmployeeQuery(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<IEnumerable<IEmployee>> Execute(EmployeeFilter filter, CancellationToken cancellationToken)
        {
            var items = await DbContext.Employees
                .AsNoTracking()
                .Where(EmployeeSpecs.ByFilter(filter))
                .OrderBy(x => x.Name)
                .Page(filter)
                .ToListAsync(cancellationToken);

            return Mapper.Map<IEnumerable<IEmployee>>(items);
        }
    }
}