using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Timesheets.Jira.Persistence.Models
{
    public class DbIssue : IEntityTypeConfiguration<DbIssue>
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Summary { get; set; }
        public string AccountKey { get; set; }

        public void Configure(EntityTypeBuilder<DbIssue> builder)
        {
            builder.ToTable("Issues");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Key).HasMaxLength(32);
            builder.Property(x => x.Summary).HasMaxLength(1024);
            builder.Property(x => x.AccountKey).HasMaxLength(DbAccount.KeyMaxLength);
        }
    }
}