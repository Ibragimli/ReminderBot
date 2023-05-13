using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderBot.Services.DTOs.Reminder
{
    public class ReminderDeleteDto
    {
        public List<int> Ids { get; set; }
    }
    public class ReminderDeleteDtoValidator : AbstractValidator<ReminderDeleteDto>
    {
        public ReminderDeleteDtoValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty().WithMessage("The ids field must not be empty.");

        }

    }
}
