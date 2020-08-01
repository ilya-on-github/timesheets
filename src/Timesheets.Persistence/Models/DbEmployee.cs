using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheets.Services.Queries.Employees;

namespace Timesheets.Persistence.Models
{
    public class DbEmployee : IEntityTypeConfiguration<DbEmployee>, IEmployee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


        public void Configure(EntityTypeBuilder<DbEmployee> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(256);
        }
    }
}