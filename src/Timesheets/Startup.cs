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
                    new QueryToApiMappingProfile()
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