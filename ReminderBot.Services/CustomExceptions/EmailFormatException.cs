using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderBot.Services.CustomExceptions
{
     
    public class EmailFormatException : Exception
    {
        public EmailFormatException(string msg) : base(msg) { }
    }
}
