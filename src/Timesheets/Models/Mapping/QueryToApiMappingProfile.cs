using AutoMapper;
using Timesheets.Models.Accounts;
using Timesheets.Models.Employees;
using Timesheets.Models.Issues;
using Timesheets.Services.Queries.Accounts;
using Timesheets.Services.Queries.Employees;
using Timesheets.Services.Queries.Issues;

namespace Timesheets.Models.Mapping
{
    public class QueryToApiMappingProfile : Profile
    {
        public QueryToApiMappingProfile()
        {
            CreateMap<IAccount, AccountDto>();
            CreateMap<IEmployee, EmployeeDto>();
            CreateMap<IIssue, IssueDto>();
        }
    }
}