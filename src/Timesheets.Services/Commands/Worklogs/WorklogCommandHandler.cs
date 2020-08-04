using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timesheets.Services.Commands.Employees;
using Timesheets.Services.Commands.Issues;

namespace Timesheets.Services.Commands.Worklogs
{
    // ReSharper disable once UnusedType.Global
    public class WorklogCommandHandler : IRequestHandler<CreateWorklogCommand, Guid>,
        IRequestHandler<UpdateWorklogCommand>, IRequestHandler<DeleteWorklogCommand>
    {
        private readonly IIssueService _issueService;
        private readonly IEmployeeService _employeeService;
        private readonly IWorklogRepository _worklogRepository;

        public WorklogCommandHandler(IMediator mediator, IIssueService issueService, IEmployeeService employeeService,
            IWorklogRepository worklogRepository)
        {
            _issueService = issueService ?? throw new ArgumentNullException(nameof(issueService));
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            _worklogRepository = worklogRepository ?? throw new ArgumentNullException(nameof(worklogRepository));
        }

        public async Task<Guid> Handle(CreateWorklogCommand request, CancellationToken cancellationToken)
        {
            var options = await GetWorklogOptions(request.Options, cancellationToken);
            var worklog = new Worklog(options);

            await _worklogRepository.Save(worklog, cancellationToken);

            return worklog.Id;
        }

        public async Task<Unit> Handle(UpdateWorklogCommand request, CancellationToken cancellationToken)
        {
            var worklog = await _worklogRepository.Get(request.Id, cancellationToken);
            var options = await GetWorklogOptions(request.Options, cancellationToken);

            worklog.Update(options);

            await _worklogRepository.Save(worklog, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteWorklogCommand request, CancellationToken cancellationToken)
        {
            await _worklogRepository.Delete(request.Id, cancellationToken);

            return Unit.Value;
        }

        private async Task<WorklogOptions> GetWorklogOptions(SetWorklogOptions command,
            CancellationToken cancellationToken)
        {
            var employee = await _employeeService.GetEmployeeDescriptor(command.EmployeeId, cancellationToken);
            var issue = await _issueService.GetIssueDescriptor(command.IssueId, cancellationToken);

            var worklogOptions = new WorklogOptions
            {
                Employee = employee,
                Issue = issue,
                Started = command.Started,
                TimeSpent = command.TimeSpent,
                WorkDescription = command.WorkDescription
            };

            return worklogOptions;
        }
    }
}