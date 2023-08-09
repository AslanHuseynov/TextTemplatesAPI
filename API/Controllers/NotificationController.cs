﻿using Company.Application.Interfaces;
using Company.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("SendSms")]
        public async Task<ActionResult<List<Template>>> SendSMS(int employeeId, string number, string user)
        {
            var result = await _notificationService.SendSMS(employeeId, number, user);
            return Ok(result);
        }

        [HttpPost("SendMail")]
        public async Task<ActionResult<List<Template>>> SendMail(int employeeId, string mail, string user)
        {
            var result = await _notificationService.SendMail(employeeId, mail, user);
            return Ok(result);
        }

    }
}
