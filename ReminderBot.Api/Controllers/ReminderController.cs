﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReminderBot.Core.Entities;
using ReminderBot.Services.CustomExceptions;
using ReminderBot.Services.DTOs;
using ReminderBot.Services.DTOs.Reminder;
using ReminderBot.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReminderBot.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly IReminderServices _reminderServices;

        public ReminderController(IReminderServices reminderServices)
        {
            _reminderServices = reminderServices;
        }

        [HttpPost("{create}")]
        public async Task<IActionResult> Create([FromBody] ReminderPostDto reminderPostDto)
        {
            Reminder reminder = new Reminder();
            try
            {
                reminder = await _reminderServices.CreateReminder(reminderPostDto);
            }
            catch (ReminderNullException e)
            {
                return StatusCode(404, e.Message);
            }
            catch (EmailFormatException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (ValueFormatException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (DateFormatException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(404, e.Message);
            }
            return StatusCode(202, reminder);
        }

        [HttpPut("{update}")]
        public async Task<IActionResult> Update([FromBody] ReminderPutDto reminderPutDto)
        {
            Reminder reminder = new Reminder();
            try
            {
                reminder = await _reminderServices.UpdateReminder(reminderPutDto);
            }
            catch (ReminderNullException e)
            {
                return StatusCode(404, e.Message);
            }
            catch (ReminderAlreadySendException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (EmailFormatException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (ValueFormatException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (DateFormatException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(404, e.Message);
            }
            return StatusCode(202, reminder);
        }

        [HttpDelete("{ids}")]
        public async Task<IActionResult> Delete([FromBody] ReminderDeleteDto reminderDeleteDto)
        {
            try
            {
                await _reminderServices.DeleteReminders(reminderDeleteDto);
            }
            catch (ReminderNullException e)
            {
                return StatusCode(404, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(404, e.Message);
            }
            return StatusCode(202, "Delete successful");
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(int page = 1, string method = null)
        {
            PagenatedListDto<ReminderListItemDto> reminders;
            try
            {
                reminders = await _reminderServices.GetAllReminder(page, method);
            }
            catch (PageIndexFormatException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (ValueFormatException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(404, e.Message);
            }
            return StatusCode(202, reminders);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ReminderGetDto reminder = new ReminderGetDto();
            try
            {
                reminder = await _reminderServices.GetReminder(id);

            }
            catch (ReminderNotFoundException e)
            {
                return StatusCode(404, e.Message);
            }
            catch (ReminderAlreadySendException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(404, e.Message);
            }
            return StatusCode(202, reminder);
        }

    }
}
