using System;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Timesheets.Models.Mapping;
using Timesheets.Persistence;
using Timesheets.Persistence.Queries;
using Timesheets.Persistence.Queries.Mapping;
using Timesheets.Persistence.Repositories;
using Timesheets.Pipeline;
using Timesheets.Services.Commands.Accounts;
using Timesheets.Services.Commands.Employees;
using Timesheets.Services.Commands.Issues;
using Timesheets.Services.Queries.Accounts;

namespace Timesheets
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(new Profile[]
                {
                    new ApiToQueryMappingProfile(),
                    new QueryToApiMappingProfile(),
                    new ApiToCommandMappingProfile(),
                    new CommandToPersistenceMappingProfile(),
                    new PersistenceToCommandMappingProfile(),
                    new PersistenceToQueryMappingProfile()
                });
            });
            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);

            services.AddControllers();

            services.AddDbContext<AppDbContext>(ops => { ops.UseNpgsql(_configuration.GetConnectionString("main")); });

            services.AddMediatR(
                typeof(AccountQuery).Assembly,
                typeof(AccountQueryHandler).Assembly
            );

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IIssueRepository, IssueRepository>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionalPipelineBehavior<,>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}