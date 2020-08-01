using AutoMapper;
using Timesheets.Persistence.Models;
using Timesheets.Services.Commands.Accounts;

namespace Timesheets.Persistence.Queries.Mapping
{
    public class CommandToPersistenceMappingProfile : Profile
    {
        public CommandToPersistenceMappingProfile()
        {
            CreateMap<Account, DbAccount>();
        }
    }
}