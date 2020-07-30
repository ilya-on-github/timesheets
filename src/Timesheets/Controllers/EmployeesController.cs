using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Models;
using Timesheets.Services.Queries.Employees;

namespace Timesheets.Controllers
{
    [Route("api/employees")]
    public class EmployeesController : Controller
    {
        public EmployeesController(IMapper mapper, IMediator mediator)
            : base(mapper, mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EmployeeDto>), 200)]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeeFilterDto filterDto,
            CancellationToken cancellationToken)
        {
            var filter = Mapper.Map<EmployeeFilter>(filterDto);
            var employees = await Mediator.Send(new EmployeeQuery(filter), cancellationToken);

            return Ok(Mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }
    }
}