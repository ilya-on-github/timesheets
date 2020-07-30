using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Services.Queries;

namespace Timesheets.Controllers
{
    public abstract class Controller : ControllerBase
    {
        protected readonly IMapper Mapper;

        protected Controller(IMapper mapper)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}