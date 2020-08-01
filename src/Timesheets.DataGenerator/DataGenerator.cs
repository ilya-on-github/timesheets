using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Timesheets.Persistence;
using Timesheets.Persistence.Models;

namespace Timesheets.DataGenerator
{
    [TestFixture]
    public class DataGenerator
    {
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();

            _fixture.Register(() =>
            {
                var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseNpgsql("User ID=admin;Password=admin;Host=localhost;Port=5432;Database=timesheets;")
                    .Options;

                var dbContext = new AppDbContext(options);

                return dbContext;
            });

            _fixture.Freeze<AppDbContext>();
        }

        [Test]
        public void GenerateAccounts()
        {
            var accounts = _fixture.CreateMany<DbAccount>();

            Assert.DoesNotThrowAsync(async () => { await Add(accounts, CancellationToken.None); });
        }

        [Test]
        public void GenerateEmployees()
        {
            var employees = _fixture.CreateMany<DbEmployee>();

            Assert.DoesNotThrowAsync(async () => { await Add(employees, CancellationToken.None); });
        }

        [Test]
        public async Task GenerateIssues()
        {
            var accounts = await Get<DbAccount>(CancellationToken.None);

            var issues = accounts.SelectMany(a =>
                _fixture.Build<DbIssue>()
                    .With(i => i.AccountId, a.Id)
                    .CreateMany()
            );

            Assert.DoesNotThrowAsync(async () => { await Add(issues, CancellationToken.None); });
        }

        [Test]
        public async Task GenerateWorklogs()
        {
            var employees = await Get<DbEmployee>(CancellationToken.None);
            var issues = await Get<DbIssue>(CancellationToken.None);

            var workLogs = employees
                .RandomSubset(80)
                .SelectMany(e =>
                    _fixture.Build<DbWorklog>()
                        .With(w => w.EmployeeId, e.Id)
                        .With(w => w.IssueId, issues.Random().Id)
                        .With(w => w.TimeSpent, () => TimeSpan.FromMinutes(_fixture.Create<int>()))
                        .CreateMany()
                );

            Assert.DoesNotThrowAsync(async () => { await Add(workLogs, CancellationToken.None); });
        }

        public async Task<IEnumerable<T>> Get<T>(CancellationToken cancellationToken)
            where T : class
        {
            var dbContext = _fixture.Create<AppDbContext>();

            var items = await dbContext.Set<T>().ToListAsync(cancellationToken);

            return items;
        }

        public async Task Add<T>(IEnumerable<T> items, CancellationToken cancellationToken)
            where T : class
        {
            var dbContext = _fixture.Create<AppDbContext>();

            await dbContext.Set<T>().AddRangeAsync(items, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}