using Microsoft.Extensions.DependencyInjection;
using ReminderBot.Core;
using ReminderBot.Core.Repositories;
using ReminderBot.Data;
using ReminderBot.Data.Repositories;
using ReminderBot.Services.Profiles;
using ReminderBot.Services.Services.Implementations;
using ReminderBot.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReminderBot.Api.ServiceExtentions
{
    public static class ServiceScopeExtention
    {
        public static void AddServiceScopeExtention(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IReminderRepository, ReminderRepository>();
            services.AddScoped<IReminderServices, ReminderServices>();

            services.AddScoped<IEmailServices, EmailServices>(); 
            services.AddAutoMapper(opt => { opt.AddProfile(new AppProfile()); });

        }
    }
}
