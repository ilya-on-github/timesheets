using System.Linq;
using Timesheets.Services.Queries;

namespace Timesheets.Persistence.Queries
{
    public static class QueryableExtensions
    {
        const int TakeDefault = 20;
        private const int TakeMax = 100;

        public static IQueryable<T> Page<T>(this IOrderedQueryable<T> source, PageFilter paging)
        {
            var skip = paging.Offset ?? 0;
            int take = default;
            if (paging.Count > 0 && paging.Count < TakeMax)
            {
                take = paging.Count.Value;
            }

            return source
                .Skip(paging.Offset ?? 0)
                .Take(take);
        }
    }
}