using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Timesheets.Jira.BackgroundServices
{
    public class ProcessWorklogBackgroundService: BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new System.NotImplementedException();
        }
    }
}