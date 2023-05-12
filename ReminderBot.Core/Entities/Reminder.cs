using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderBot.Core.Entities
{
    public class Reminder : BaseEntity
    {
        public string To { get; set; }
        public string Content { get; set; }
        public DateTime SendAt { get; set; }
        public string Method { get; set; }
    }
}
