using System;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Timesheets.Controllers
{
    public abstract class Controller : ControllerBase
    {
        protected readonly IMapper Mapper;
        protected readonly IMediator Mediator;

        protected Controller(IMapper mapper, IMediator mediator)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
    }
}