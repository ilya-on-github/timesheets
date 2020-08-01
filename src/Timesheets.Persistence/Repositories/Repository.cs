using System;
using AutoMapper;

namespace Timesheets.Persistence.Repositories
{
    public abstract class Repository
    {
        protected readonly AppDbContext DbContext;
        protected readonly IMapper Mapper;

        protected Repository(AppDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}