using AutoMapper;
using Timesheets.Persistence.Models;
using Timesheets.Services.Commands.Accounts;
using Timesheets.Services.Commands.Employees;
using Timesheets.Services.Commands.Issues;

namespace Timesheets.Persistence.Queries.Mapping
{
    public class PersistenceToCommandMappingProfile : Profile
    {
        public PersistenceToCommandMappingProfile()
        {
            CreateMap<DbAccount, Account>()
                .ConstructUsing(src => new Account(src.Id, src.Name))
                .ForAllMembers(cfg => cfg.Ignore());

            CreateMap<DbEmployee, Employee>()
                .ConstructUsing(src => new Employee(src.Id, src.Name))
                .ForAllMembers(cfg => cfg.Ignore());

            CreateMap<DbIssue, Issue>()
                .ConstructUsing(src => new Issue(src.Id, src.Summary, src.Description, src.AccountId))
                .ForAllMembers(cfg => cfg.Ignore());
        }
    }
}