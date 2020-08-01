using AutoMapper;
using Timesheets.Models.Accounts;
using Timesheets.Models.Employees;
using Timesheets.Services.Queries.Accounts;
using Timesheets.Services.Queries.Employees;

namespace Timesheets.Models.Mapping
{
    public class QueryToApiMappingProfile : Profile
    {
        public QueryToApiMappingProfile()
        {
            CreateMap<IAccount, AccountDto>();
            CreateMap<IEmployee, EmployeeDto>();
        }
    }
}