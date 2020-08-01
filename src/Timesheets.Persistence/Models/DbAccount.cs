using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheets.Services.Queries.Accounts;

namespace Timesheets.Persistence.Models
{
    public class DbAccount : IEntityTypeConfiguration<DbAccount>, IAccount
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public void Configure(EntityTypeBuilder<DbAccount> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(256);
        }
    }
}