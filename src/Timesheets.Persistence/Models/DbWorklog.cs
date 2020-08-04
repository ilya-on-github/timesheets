﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheets.Services.Queries.Employees;
using Timesheets.Services.Queries.Issues;
using Timesheets.Services.Queries.Worklogs;

namespace Timesheets.Persistence.Models
{
    public class DbWorklog : IEntityTypeConfiguration<DbWorklog>, IWorklog
    {
        public Guid Id { get; set; }

        public DateTimeOffset Started { get; set; }
        public TimeSpan TimeSpent { get; set; }
        public string WorkDescription { get; set; }

        public Guid EmployeeId { get; set; }
        public DbEmployee Employee { get; set; }

        public Guid IssueId { get; set; }
        public DbIssue Issue { get; set; }

        IIssue IWorklog.Issue => Issue;
        IEmployee IWorklog.Employee => Employee;

        public void Configure(EntityTypeBuilder<DbWorklog> builder)
        {
            builder.ToTable("Worklogs");
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