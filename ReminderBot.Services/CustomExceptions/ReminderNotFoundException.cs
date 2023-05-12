using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderBot.Services.CustomExceptions
{

    public class ReminderNotFoundException : Exception
    {
        public ReminderNotFoundException(string msg) : base(msg) { }
    }
}
