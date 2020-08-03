using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Models.Employees;
using Timesheets.Services.Commands.Employees;
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

        [HttpPost]
        [ProducesResponseType(typeof(EmployeeDto), 200)]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto,
            CancellationToken cancellationToken)
        {
            var command = new CreateEmployeeCommand(createEmployeeDto.Name);
            var employeeId = await Mediator.Send(command, cancellationToken);

            return Ok(await GetEmployee(employeeId, cancellationToken));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EmployeeDto), 200)]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id,
            [FromBody] UpdateEmployeeDto updateEmployeeDto, CancellationToken cancellationToken)
        {
            var command = new UpdateEmployeeCommand(id, updateEmployeeDto.Name);
            await Mediator.Send(command, cancellationToken);

            return Ok(await GetEmployee(id, cancellationToken));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteEmployeeCommand(id);
            await Mediator.Send(command, cancellationToken);

            return Ok();
        }

        private async Task<EmployeeDto> GetEmployee(Guid id, CancellationToken cancellationToken)
        {
            var employee = await Mediator.Send(new SingleEmployeeQuery(id), cancellationToken);
            return Mapper.Map<EmployeeDto>(employee);
        }
    }
}