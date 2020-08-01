using AutoMapper;
using Timesheets.Persistence.Models;
using Timesheets.Services.Commands.Accounts;
using Timesheets.Services.Commands.Employees;

namespace Timesheets.Persistence.Queries.Mapping
{
    public class CommandToPersistenceMappingProfile : Profile
    {
        public CommandToPersistenceMappingProfile()
        {
            CreateMap<Account, DbAccount>();
            CreateMap<Employee, DbEmployee>();
        }
    }
}