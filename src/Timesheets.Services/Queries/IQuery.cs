using System.Threading;
using System.Threading.Tasks;

namespace Timesheets.Services.Queries
{
    public interface IQuery<in TFilter, TResult>
    {
        Task<TResult> Execute(TFilter filter, CancellationToken cancellationToken);
    }
}