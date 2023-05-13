using ReminderBot.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System;
using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace ReminderBot.Services.Services.Implementations
{
    public class ReminderBackgroundServices : IReminderBackgroundServices
    {
        public ReminderBackgroundServices(IReminderServices reminderServices)
        {
            _reminderServices = reminderServices;
        }
        private readonly IReminderServices _reminderServices;

        public Task StartScheduledExecution()
        {
            throw new NotImplementedException();
        }


      
    }


}



