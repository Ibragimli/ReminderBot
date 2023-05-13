using FluentValidation;
using ReminderBot.Services.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderBot.Services.DTOs.Reminder
{

    public class ReminderPutDto
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
        public DateTime SendAt { get; set; }
        public string Method { get; set; }
    }
    public class ReminderPutDtoValidator : AbstractValidator<ReminderPutDto>
    {
        public ReminderPutDtoValidator()
        {
            RuleFor(x => x.Id)
             .NotEmpty().WithMessage("The id field must not be empty.");
            RuleFor(x => x.To)
                .NotEmpty().WithMessage("Parameter should be a valid email address for email reminders, or a chat ID for Telegram reminders.")
                .MaximumLength(45).WithMessage("The length of the 'to' cannot exceed 45!");
            RuleFor(x => x.Method)
                .NotEmpty().WithMessage("The method parameter should be either 'email' or 'telegram'!")
                .MaximumLength(10).WithMessage("The length of the method cannot exceed 10!");
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("The content field must not be empty.")
                .MaximumLength(500).WithMessage("The length of the content cannot exceed 500!");
            RuleFor(x => x.SendAt).NotEmpty().WithMessage("The date field must not be empty.")
                .Must(BeGreaterThanDate).WithMessage("Parameter must be greater than the current date!");
        }

        private bool BeGreaterThanDate(DateTime sendAt)
        {
            if (!DateTime.TryParse(sendAt.ToString(), out DateTime parsedDate))
                throw new DateFormatException("Date format is invalid!");
            return parsedDate > DateTime.UtcNow.AddHours(4);
        }


    }
}
