using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Timesheets.Jira.Persistence.Models
{
    public class DbWorklog : DbExternalEntity, IEntityTypeConfiguration<DbWorklog>
    {
        public int TempoWorklogId { get; set; }
        public string Worker { get; set; }

        public DbIssue Issue { get; set; }
        public int IssueId { get; set; }

        public DateTimeOffset Started { get; set; }
        public int TimeSpentSeconds { get; set; }
        public string Comment { get; set; }

        public void Configure(EntityTypeBuilder<DbWorklog> builder)
        {
            builder.ToTable("Worklogs");

            builder.HasKey(x => x.TempoWorklogId);

            builder.Property(x => x.TempoWorklogId).ValueGeneratedNever();
            builder.Property(x => x.Worker).HasMaxLength(128);

            builder.HasOne(x => x.Issue)
                .WithMany()
                .HasForeignKey(x => x.IssueId);
        }
    }
}