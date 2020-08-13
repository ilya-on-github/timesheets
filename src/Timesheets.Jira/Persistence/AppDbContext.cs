using Microsoft.EntityFrameworkCore;
using Timesheets.Jira.Persistence.Models;

namespace Timesheets.Jira.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<DbWorklog> Worklogs { get; set; }
        public DbSet<DbIssue> Issues { get; set; }
        public DbSet<DbAccount> Accounts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}