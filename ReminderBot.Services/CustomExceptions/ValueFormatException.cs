using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderBot.Services.CustomExceptions
{

    public class ValueFormatException : Exception
    {
        public ValueFormatException(string msg) : base(msg) { }
    }
}
