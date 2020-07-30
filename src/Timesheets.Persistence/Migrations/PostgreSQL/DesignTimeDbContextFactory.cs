using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Timesheets.Persistence.Migrations.PostgreSQL
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql("User ID=admin;Password=admin;Host=localhost;Port=5432;Database=timesheets;")
                .Options;

            return new AppDbContext(options);
        }
    }
}