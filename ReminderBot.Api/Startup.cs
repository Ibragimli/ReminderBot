using AspNetCoreRateLimit;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ReminderBot.Api.ServiceExtentions;
using ReminderBot.Data;
using ReminderBot.Services.DTOs.Reminder;
using ReminderBot.Services.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReminderBot.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ReminderPostDto>());
           
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Default")));
          
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>(); // IProcessingStrategy'yi AsyncKeyLockProcessingStrategy ile değiştirdik
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();


            //services.AddHangfire(config => config.UseSqlServerStorage(Configuration.GetConnectionString("Default")));

            //services.AddHangfire(x =>
            //{
            //    x.UseSqlServerStorage(Configuration.GetConnectionString("Default"));
            //    RecurringJob.AddOrUpdate<Job>(j => j.DBControl(), "*/2 * * * *");
            //});
            //services.AddHangfireServer();

            services.AddServiceScopeExtention();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.AddExceptionHandlerService();

            app.UseRouting();

            app.UseAuthorization();
            //app.UseHangfireDashboard();
            app.UseIpRateLimiting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
