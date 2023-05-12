using ReminderBot.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReminderBot.Core
{
    public interface IUnitOfWork
    {
        IReminderRepository ReminderRepository { get; }
        Task<int> CommitAsync();
    }
}
