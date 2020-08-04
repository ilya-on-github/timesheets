using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Models.Worklogs;
using Timesheets.Services.Commands.Worklogs;
using Timesheets.Services.Queries.Worklogs;

namespace Timesheets.Controllers
{
    [Route("api/worklogs")]
    public class WorklogsController : Controller
    {
        public WorklogsController(IMapper mapper, IMediator mediator)
            : base(mapper, mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WorklogDto>), 200)]
        public async Task<IActionResult> GetWorklogs([FromQuery] WorklogFilterDto filterDto,
            CancellationToken cancellationToken)
        {
            var query = new WorklogQuery(Mapper.Map<WorklogFilter>(filterDto));
            var worklogs = await Mediator.Send(query, cancellationToken);

            return Ok(Mapper.Map<IEnumerable<WorklogDto>>(worklogs));
        }

        [HttpPost]
        [ProducesResponseType(typeof(WorklogDto), 200)]
        public async Task<IActionResult> CreateWorklog([FromBody] CreateWorklogDto createWorklogDto,
            CancellationToken cancellationToken)
        {
            var command = new CreateWorklogCommand(Mapper.Map<SetWorklogOptions>(createWorklogDto));
            var worklogId = await Mediator.Send(command, cancellationToken);

            return await GetWorklog(worklogId, cancellationToken);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(WorklogDto), 200)]
        public async Task<IActionResult> UpdateWorklog([FromRoute] Guid id,
            [FromBody] UpdateWorklogDto updateWorklogDto, CancellationToken cancellationToken)
        {
            var command = new UpdateWorklogCommand(id, Mapper.Map<SetWorklogOptions>(updateWorklogDto));
            await Mediator.Send(command, cancellationToken);

            return await GetWorklog(id, cancellationToken);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteWorklog([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteWorklogCommand(id);
            await Mediator.Send(command, cancellationToken);

            return Ok();
        }

        private async Task<IActionResult> GetWorklog(Guid id, CancellationToken cancellationToken)
        {
            var query = new SingleWorklogQuery(id);
            var worklog = await Mediator.Send(query, cancellationToken);

            return Ok(Mapper.Map<WorklogDto>(worklog));
        }
    }
}