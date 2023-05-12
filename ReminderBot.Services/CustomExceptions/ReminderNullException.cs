using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderBot.Services.CustomExceptions
{

    public class ReminderNullException : Exception
    {
        public ReminderNullException(string msg) : base(msg) { }
    }
}
