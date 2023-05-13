using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ReminderBot.Core;
using ReminderBot.Core.Entities;
using ReminderBot.Services.CustomExceptions;
using ReminderBot.Services.DTOs;
using ReminderBot.Services.DTOs.Reminder;
using ReminderBot.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReminderBot.Services.Services.Implementations
{
    public class ReminderServices : IReminderServices
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ReminderServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Reminder> CreateReminder(ReminderPostDto reminderPostDto)
        {
            if (reminderPostDto.Method == "telegram" || reminderPostDto.Method == "email")
            {
                if (reminderPostDto.Method.ToLower() == "email")
                    if (!IsValidEmail(reminderPostDto.To))
                        throw new EmailFormatException("Please enter a valid email!");
                var reminder = _mapper.Map<Reminder>(reminderPostDto);
                await _unitOfWork.ReminderRepository.InsertAsync(reminder);
                await _unitOfWork.CommitAsync();
                return reminder;
            }
            throw new ValueFormatException("The method parameter should be either 'email' or 'telegram' ");
        }

        public async Task<PagenatedListDto<ReminderListItemDto>> GetAllReminder(int page, string method)
        {

            if (page < 1) throw new PageIndexFormatException("The number of pages can be 1 or more than 1!");
            if (method?.Length > 11) throw new PageIndexFormatException("The length of the method cannot exceed 10!");

            IEnumerable<Reminder> appUsers = await _unitOfWork.ReminderRepository.GetAllPagenatedAsync(x => string.IsNullOrWhiteSpace(method) ? true : x.Method.ToLower().Contains(method) == false && !x.IsDelete, page, 5);
            int totalCount = await _unitOfWork.ReminderRepository.GetTotalCountAsync(x => string.IsNullOrWhiteSpace(method) ? true : x.Method.ToLower().Contains(method) && !x.IsDelete);

            IEnumerable<ReminderListItemDto> itemDtos = _mapper.Map<IEnumerable<ReminderListItemDto>>(appUsers);
            PagenatedListDto<ReminderListItemDto> model = new PagenatedListDto<ReminderListItemDto>(itemDtos, totalCount, page, 2);

            return model;
        }

        public async Task<ReminderGetDto> GetReminder(int id)
        {
            Reminder reminderExist = await _unitOfWork.ReminderRepository.GetAsync(x => !x.IsDelete && x.Id == id);
            if (reminderExist == null)
                throw new ReminderNotFoundException("Reminder not found!");
            ReminderGetDto userDto = new ReminderGetDto();
            userDto = _mapper.Map<ReminderGetDto>(reminderExist);
            return userDto;
        }

        public async Task<Reminder> UpdateReminder(ReminderPutDto reminderPutDto)
        {

            Reminder reminderExist = await _unitOfWork.ReminderRepository.GetAsync(x => x.Id == reminderPutDto.Id && !x.IsDelete);
            if (reminderExist == null)
                throw new ReminderNullException("Reminder notfound error!");

            if (reminderPutDto.Method == "telegram" || reminderPutDto.Method == "email")
            {
                if (reminderPutDto.Method.ToLower() == "email")
                    if (!IsValidEmail(reminderPutDto.To))
                        throw new EmailFormatException("Please enter a valid email!");
                reminderExist.Method = reminderPutDto.Method;
                reminderExist.To = reminderPutDto.To;
                reminderExist.SendAt = reminderPutDto.SendAt;
                reminderExist.Content = reminderPutDto.Content;
                reminderExist.ModifiedDate = DateTime.UtcNow.AddHours(4);

                await _unitOfWork.CommitAsync();
                return reminderExist;
            }
            throw new ValueFormatException("The method parameter should be either 'email' or 'telegram' ");
        }

        private bool IsValidEmail(string value)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            return Regex.IsMatch(value, pattern);
        }
    }
}
