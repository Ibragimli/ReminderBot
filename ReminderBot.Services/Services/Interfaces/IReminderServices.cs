using ReminderBot.Core.Entities;
using ReminderBot.Services.DTOs;
using ReminderBot.Services.DTOs.Reminder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReminderBot.Services.Services.Interfaces
{
    public interface IReminderServices
    {
        public Task<PagenatedListDto<ReminderListItemDto>> GetAllReminder(int page, string search);
        public Task<ReminderGetDto> GetReminder(int id);
        public Task SendMessage();
        public Task<Reminder> CreateReminder(ReminderPostDto reminderPostDto);
        public Task<Reminder> UpdateReminder(ReminderPutDto reminderPutDto);
        public Task DeleteReminders(ReminderDeleteDto reminderDeleteDto);

    }
}
