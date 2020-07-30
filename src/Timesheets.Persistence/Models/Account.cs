using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheets.Services.Queries.Accounts;

namespace Timesheets.Persistence.Models
{
    public class Account : IEntityTypeConfiguration<Account>, IAccount
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(256);
        }
    }
}