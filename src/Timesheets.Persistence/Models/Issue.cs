using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Timesheets.Persistence.Models
{
    public class Issue : IEntityTypeConfiguration<Issue>
    {
        public Guid Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }

        public Guid AccountId { get; set; }
        public Account Account { get; set; }

        public void Configure(EntityTypeBuilder<Issue> builder)
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