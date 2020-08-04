using System;
using AutoMapper;

namespace Timesheets.Persistence.Queries
{
    public abstract class QueryHandler
    {
        protected AppDbContext DbContext;
        protected IMapper Mapper;

        protected QueryHandler(AppDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}