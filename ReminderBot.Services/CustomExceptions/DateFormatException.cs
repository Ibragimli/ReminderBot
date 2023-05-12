using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderBot.Services.CustomExceptions
{

    public class DateFormatException : Exception
    {
        public DateFormatException(string msg) : base(msg) { }
    }
}
