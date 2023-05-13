using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReminderBot.Services.Services.Interfaces
{
    public interface IReminderBackgroundServices
    {
        public  Task StartScheduledExecution();
    }
}
