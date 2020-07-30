﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Timesheets.Persistence.Models
{
    public class WorkLog : IEntityTypeConfiguration<WorkLog>
    {
        public Guid Id { get; set; }

        public DateTimeOffset Started { get; set; }
        public TimeSpan TimeSpent { get; set; }
        public string WorkDescription { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public Guid IssueId { get; set; }
        public Issue Issue { get; set; }

        public void Configure(EntityTypeBuilder<WorkLog> builder)
        {
            builder.ToTable("WorkLogs");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId);

            builder.HasOne(x => x.Issue)
                .WithMany()
                .HasForeignKey(x => x.IssueId);
        }
    }
}