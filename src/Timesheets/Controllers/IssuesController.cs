using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Models.Issues;
using Timesheets.Services.Commands.Issues;
using Timesheets.Services.Queries.Issues;

namespace Timesheets.Controllers
{
    [Route("api/issues")]
    public class IssuesController : Controller
    {
        public IssuesController(IMapper mapper, IMediator mediator)
            : base(mapper, mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<IssueDto>), 200)]
        public async Task<IActionResult> GetIssues([FromQuery] IssueFilterDto filterDto,
            CancellationToken cancellationToken)
        {
            var request = new IssueQuery(Mapper.Map<IssueFilter>(filterDto));
            var issues = await Mediator.Send(request, cancellationToken);

            return Ok(Mapper.Map<IEnumerable<IIssue>>(issues));
        }

        [HttpPost]
        [ProducesResponseType(typeof(IssueDto), 200)]
        public async Task<IActionResult> CreateIssue([FromBody] CreateIssueDto createIssueDto,
            CancellationToken cancellationToken)
        {
            var request = new CreateIssueCommand(createIssueDto.Summary, createIssueDto.Description,
                createIssueDto.Account.Id);

            var issueId = await Mediator.Send(request, cancellationToken);

            return Ok(await GetIssue(issueId, cancellationToken));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IssueDto), 200)]
        public async Task<IActionResult> UpdateIssue([FromRoute] Guid id, [FromBody] UpdateIssueDto updateIssueDto,
            CancellationToken cancellationToken)
        {
            var request = new UpdateIssueCommand(id, updateIssueDto.Summary, updateIssueDto.Description,
                updateIssueDto.Account?.Id);

            await Mediator.Send(request, cancellationToken);

            return Ok(await GetIssue(id, cancellationToken));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteIssue([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteIssueCommand(id);

            await Mediator.Send(request, cancellationToken);

            return Ok();
        }

        private async Task<IssueDto> GetIssue(Guid id, CancellationToken cancellationToken)
        {
            var issue = await Mediator.Send(new SingleIssueQuery(id), cancellationToken);
            return Mapper.Map<IssueDto>(issue);
        }
    }
}