using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderBot.Services.DTOs.Reminder
{
    public class ReminderGetDto
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
        public DateTime SendAt { get; set; }
        public string Method { get; set; }
    }
}
