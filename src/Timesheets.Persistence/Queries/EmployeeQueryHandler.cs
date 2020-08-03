using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Timesheets.Services.Queries.Employees;

namespace Timesheets.Persistence.Queries
{
    public class EmployeeQueryHandler : Query, IRequestHandler<EmployeeQuery, IEnumerable<IEmployee>>,
        IRequestHandler<SingleEmployeeQuery, IEmployee>
    {
        public EmployeeQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<IEnumerable<IEmployee>> Handle(EmployeeQuery request, CancellationToken cancellationToken)
        {
            var items = await DbContext.Employees
                .AsNoTracking()
                .Where(EmployeeSpecs.ByFilter(request.Filter))
                .OrderBy(x => x.Name)
                .Page(request.Filter)
                .ToListAsync(cancellationToken);

            return Mapper.Map<IEnumerable<IEmployee>>(items);
        }

        public async Task<IEmployee> Handle(SingleEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employee = await DbContext.Employees
                .FirstOrDefaultAsync(EmployeeSpecs.ById(request.Id), cancellationToken);

            return Mapper.Map<IEmployee>(employee);
        }
    }
}