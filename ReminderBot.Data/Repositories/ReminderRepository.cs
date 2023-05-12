using ReminderBot.Core.Entities;
using ReminderBot.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderBot.Data.Repositories
{
    public class ReminderRepository : Repository<Reminder>, IReminderRepository
    {
        private readonly DataContext _context;
        public ReminderRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
