using ReminderBot.Core;
using ReminderBot.Core.Repositories;
using ReminderBot.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReminderBot.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private IReminderRepository _reminderRepository;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public IReminderRepository ReminderRepository => _reminderRepository = _reminderRepository ?? new ReminderRepository(_context);
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
