using System;
using AutoMapper;
using Timesheets.Models.Worklogs;
using Timesheets.Services.Commands.Worklogs;

namespace Timesheets.Models.Mapping
{
    public class ApiToCommandMappingProfile : Profile
    {
        public ApiToCommandMappingProfile()
        {
            CreateMap<CreateWorklogDto, SetWorklogOptions>()
                .ForMember(x => x.TimeSpent, cfg => cfg.MapFrom(src => src.TimeSpentSeconds.HasValue
                    ? TimeSpan.FromSeconds(src.TimeSpentSeconds.Value)
                    : (TimeSpan?) null));

            CreateMap<UpdateWorklogDto, SetWorklogOptions>()
                .ForMember(x => x.TimeSpent, cfg => cfg.MapFrom(src => src.TimeSpentSeconds.HasValue
                    ? TimeSpan.FromSeconds(src.TimeSpentSeconds.Value)
                    : (TimeSpan?) null));
        }
    }
}