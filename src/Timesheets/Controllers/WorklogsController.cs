using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Timesheets.Controllers
{
    public class WorklogsController : Controller
    {
        public WorklogsController(IMapper mapper, IMediator mediator)
            : base(mapper, mediator)
        {
        }

        [HttpGet]
        public Task<IActionResult> GetTimesheets()
        {
            throw new NotImplementedException();
        }
    }
}