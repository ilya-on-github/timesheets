using AutoMapper;
using Timesheets.Models.Accounts;
using Timesheets.Models.Employees;
using Timesheets.Services.Queries;
using Timesheets.Services.Queries.Accounts;
using Timesheets.Services.Queries.Employees;

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
        }
    }
}