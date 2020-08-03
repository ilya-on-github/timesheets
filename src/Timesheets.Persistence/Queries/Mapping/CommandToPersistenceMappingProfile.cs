using AutoMapper;
using Timesheets.Persistence.Models;
using Timesheets.Services.Commands.Accounts;
using Timesheets.Services.Commands.Employees;
using Timesheets.Services.Commands.Issues;

namespace Timesheets.Persistence.Queries.Mapping
{
    public class CommandToPersistenceMappingProfile : Profile
    {
        public CommandToPersistenceMappingProfile()
        {
            CreateMap<Account, DbAccount>();
            CreateMap<Employee, DbEmployee>();

            CreateMap<Issue, DbIssue>()
                .ForMember(x => x.Account, cfg => cfg.Ignore());
        }
    }
}