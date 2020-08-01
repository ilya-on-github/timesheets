using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Timesheets.Persistence.Models;

namespace Timesheets.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<DbWorklog> Worklogs { get; set; }
        public DbSet<DbIssue> Issues { get; set; }
        public DbSet<DbAccount> Accounts { get; set; }
        public DbSet<DbEmployee> Employees { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Debug.WriteLine($"Created.");
        }
    }
}