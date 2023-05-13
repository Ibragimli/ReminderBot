using Microsoft.Extensions.Configuration;
using ReminderBot.Core;
using ReminderBot.Core.Entities;
using ReminderBot.Services.CustomExceptions;
using ReminderBot.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace ReminderBot.Services.Services.Implementations
{
    public class Job
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailServices _emailServices;
        private readonly IConfiguration _configuration;

        public Job(IUnitOfWork unitOfWork, IEmailServices emailServices, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _emailServices = emailServices;
            _configuration = configuration;
        }
        public async Task DBControl()
        {
            var id = await CheckSendAt();
            Reminder reminderExist = await _unitOfWork.ReminderRepository.GetAsync(x => x.Id == id);
            if (reminderExist != null)
            {
                if (reminderExist.Method == "email")
                {
                    string body = string.Empty;

                    body += "Email: " + reminderExist.To;
                    body += " Content: " + reminderExist.Content;
                    _emailServices.Send(reminderExist.To, "ReminderMessage", body);
                }
                else
                {
                    var botToken = _configuration.GetSection("TelegramBot:Token").Value;
                    var chatId = reminderExist.To;

                    var botClient = new TelegramBotClient(botToken);
                    var content = reminderExist.Content;

                    await botClient.SendTextMessageAsync(chatId, content);
                }
            }
        }
        private async Task<int> CheckSendAt()
        {
            DateTime now = DateTime.UtcNow.AddHours(4);
            Reminder reminder = new Reminder();
            if (await _unitOfWork.ReminderRepository.IsExistAsync(x => x.SendAt < now))
            {
                reminder = await _unitOfWork.ReminderRepository.GetAsync(x => x.SendAt < now);
                await _unitOfWork.CommitAsync();
            }
            return reminder.Id;
        }
    }
}
