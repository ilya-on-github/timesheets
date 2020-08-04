using AutoMapper;
using Timesheets.Models.Accounts;
using Timesheets.Models.Employees;
using Timesheets.Models.Issues;
using Timesheets.Models.Worklogs;
using Timesheets.Services.Queries.Accounts;
using Timesheets.Services.Queries.Employees;
using Timesheets.Services.Queries.Issues;
using Timesheets.Services.Queries.Worklogs;

namespace Timesheets.Models.Mapping
{
    public class QueryToApiMappingProfile : Profile
    {
        public QueryToApiMappingProfile()
        {
            CreateMap<IAccount, AccountDto>();
            CreateMap<IEmployee, EmployeeDto>();
            CreateMap<IIssue, IssueDto>();
            CreateMap<IWorklog, WorklogDto>()
                .ForMember(x => x.TimeSpentSeconds, cfg => cfg.MapFrom(src => src.TimeSpent.TotalSeconds));
        }
    }
}