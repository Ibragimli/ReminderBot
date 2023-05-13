using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderBot.Services.CustomExceptions
{

    public class ReminderAlreadySendException : Exception
    {
        public ReminderAlreadySendException(string msg) : base(msg) { }
    }
}
