using AutoMapper;
using ReminderBot.Core.Entities;
using ReminderBot.Services.DTOs.Reminder;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderBot.Services.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<Reminder, ReminderListItemDto>();
            CreateMap<Reminder, ReminderGetDto>();
            CreateMap<ReminderPostDto, Reminder>();

        }
    }
}
