using AutoMapper;
using Timesheets.Persistence.Models;
using Timesheets.Services.Commands.Accounts;

namespace Timesheets.Persistence.Queries.Mapping
{
    public class PersistenceToCommandMappingProfile : Profile
    {
        public PersistenceToCommandMappingProfile()
        {
            CreateMap<DbAccount, Account>()
                .ConstructUsing(src => new Account(src.Id, src.Name))
                .ForAllMembers(cfg => cfg.Ignore());
        }
    }
}