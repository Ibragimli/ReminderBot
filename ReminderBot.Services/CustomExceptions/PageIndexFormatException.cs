using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderBot.Services.CustomExceptions
{
    public class PageIndexFormatException : Exception
    {
        public PageIndexFormatException(string msg) : base(msg) { }
    }
}
