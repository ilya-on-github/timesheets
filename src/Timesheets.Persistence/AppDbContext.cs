using Microsoft.EntityFrameworkCore;
using Timesheets.Persistence.Models;

namespace Timesheets.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Worklog> Worklogs { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}