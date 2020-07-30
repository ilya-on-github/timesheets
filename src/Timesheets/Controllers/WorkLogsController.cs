using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Services.Queries.WorkLogs;

namespace Timesheets.Controllers
{
    public class WorkLogsController : Controller
    {
        private readonly IWorkLogQuery _workLogQuery;

        public WorkLogsController(IWorkLogQuery workLogQuery, IMapper mapper)
            : base(mapper)
        {
            _workLogQuery = workLogQuery ?? throw new ArgumentNullException(nameof(workLogQuery));
        }

        [HttpGet]
        public Task<IActionResult> GetTimesheets()
        {
            throw new NotImplementedException();
        }
    }
}