using AutoMapper;
using Timesheets.Models.Accounts;
using Timesheets.Models.Employees;
using Timesheets.Models.Issues;
using Timesheets.Services.Queries;
using Timesheets.Services.Queries.Accounts;
using Timesheets.Services.Queries.Employees;
using Timesheets.Services.Queries.Issues;

namespace Timesheets.Models.Mapping
{
    public class ApiToQueryMappingProfile : Profile
    {
        public ApiToQueryMappingProfile()
        {
            CreateMap<PageFilterDto, PageFilter>();
            CreateMap<PageQueryFilterDto, PageQueryFilter>();

            CreateMap<AccountFilterDto, AccountFilter>();
            CreateMap<EmployeeFilterDto, EmployeeFilter>();
            CreateMap<IssueFilterDto, IssueFilter>();
        }
    }
}