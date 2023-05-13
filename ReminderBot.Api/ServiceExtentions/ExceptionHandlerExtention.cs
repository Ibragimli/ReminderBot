using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ReminderBot.Services.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReminderBot.Api.ServiceExtentions
{
    public static class ExceptionHandlerExtention
    {
        public static void AddExceptionHandlerService(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var code = 500;
                    string message = "Inter Server Error. Please Try Again Later!";

                    if (contextFeature != null)
                    {
                        message = contextFeature.Error.Message;

                        if (contextFeature.Error is ReminderNullException)
                            code = 404;
                        if (contextFeature.Error is ReminderNotFoundException)
                            code = 404;
                        if (contextFeature.Error is ValueFormatException)
                            code = 400;
                        if (contextFeature.Error is EmailFormatException)
                            code = 400;
                        if (contextFeature.Error is DateFormatException)
                            code = 400;
                     
                        //if (contextFeature.Error is ImageFormatException)
                        //    code = 400;
                        //if (contextFeature.Error is ImageNullException)
                        //    code = 404;
                        //if (contextFeature.Error is AuthenticationCodeException)
                        //    code = 400;
                        //if (contextFeature.Error is CookieNotActiveException)
                        //    code = 404;
                        //if (contextFeature.Error is ItemNullException)
                        //    code = 404;
                        //if (contextFeature.Error is ItemFormatException)
                        //    code = 400;

                        //if (contextFeature.Error is NotFoundException)
                        //    code = 404;
                        //if (contextFeature.Error is PaymentValueException)
                        //    code = 400;
                        //if (contextFeature.Error is ItemAlreadyException)
                        //    code = 404;
                        //if (contextFeature.Error is ValueAlreadyExpception)
                        //    code = 404;
                        //if (contextFeature.Error is ItemUseException)
                        //    code = 500;
                        //    if (contextFeature.Error is ImageCountException)
                        //        code = 400;
                        //if (contextFeature.Error is UserNotFoundException)
                        //    code = 404;
                        //if (contextFeature.Error is UserFormatException)
                        //    code = 400;
                        if (contextFeature.Error is PageIndexFormatException)
                            code = 400;
                        //if (contextFeature.Error is ExpirationDateException)
                        //    code = 400;

                    }

                    context.Response.StatusCode = code;

                    var errprJsonStr = JsonConvert.SerializeObject(new { code = code, message = message });

                    await context.Response.WriteAsync(errprJsonStr);
                });

            });

        }
    }
}
