using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderBot.Services.DTOs.Reminder
{

    public class ReminderListItemDto
    {
        public string Id { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
        public DateTime SendAt { get; set; }
        public string Method { get; set; }
        public DateTime CreatedTime { get; set; }
    }
   
}
