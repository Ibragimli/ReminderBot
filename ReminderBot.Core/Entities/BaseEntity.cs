using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderBot.Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime ModifiedDate { get; set; }
        public bool IsDelete { get; set; } = false;
    }
}
