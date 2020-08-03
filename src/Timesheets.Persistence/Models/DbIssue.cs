using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheets.Services.Queries.Accounts;
using Timesheets.Services.Queries.Issues;

namespace Timesheets.Persistence.Models
{
    public class DbIssue : IEntityTypeConfiguration<DbIssue>, IIssue
    {
        public Guid Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }

        public Guid AccountId { get; set; }
        public DbAccount Account { get; set; }

        IAccount IIssue.Account => Account;

        public void Configure(EntityTypeBuilder<DbIssue> builder)
        {
            builder.ToTable("Issues");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Summary).HasMaxLength(512);

            builder.HasOne(x => x.Account)
                .WithMany()
                .HasForeignKey(x => x.AccountId);
        }
    }
}