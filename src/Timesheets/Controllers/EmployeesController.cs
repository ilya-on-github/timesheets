using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Models;
using Timesheets.Services.Queries.Employees;

namespace Timesheets.Controllers
{
    [Route("api/employees")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeQuery _employeeQuery;

        public EmployeesController(IMapper mapper, IEmployeeQuery employeeQuery)
            : base(mapper)
        {
            _employeeQuery = employeeQuery ?? throw new ArgumentNullException(nameof(employeeQuery));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EmployeeDto>), 200)]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeeFilterDto filterDto,
            CancellationToken cancellationToken)
        {
            var filter = Mapper.Map<EmployeeFilter>(filterDto);
            var employees = await _employeeQuery.Execute(filter, cancellationToken);

            return Ok(Mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }
    }
}