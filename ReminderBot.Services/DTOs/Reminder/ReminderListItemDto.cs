using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderBot.Services.DTOs.Reminder
{

    public class ReminderListItemDto
    {
        public string Id { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
        public DateTime SendAt { get; set; }
        public string Method { get; set; }
        public DateTime CreatedTime { get; set; }
    }
    //public class ReminderGetAllDtoValidator : AbstractValidator<ReminderGetAllDto>
    //{
    //    public ReminderGetAllDtoValidator()
    //    {
    //        RuleFor(x => x.To).NotEmpty().WithMessage("Kategoriya hissəsi boş olmamalıdır.");
    //        RuleFor(x => x.Content).NotEmpty().WithMessage("Elanın ad hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Elanın adının uzunluğu 3-dən az ola bilməz!").MaximumLength(100).WithMessage("Elanın adının uzunluğu 100 dən böyük ola bilməz!");
    //        RuleFor(x => x.SendAt).NotEmpty().WithMessage("Elanın qiyməti  boş olmamalıdır.").GreaterThan(0).WithMessage("Elanın qiyməti 0-dən az ola bilməz").LessThan(9999999).WithMessage("Elanın qiyməti 9999999-dən çox ola bilməz");
    //        RuleFor(x => x.Describe).NotEmpty().WithMessage("Elanın təsvir hissəsi boş olmamalıdır.");
    //    }
    //}
}
