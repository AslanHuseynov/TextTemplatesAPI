using AutoMapper;
using Company.Application.Interfaces;
using Company.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Notification>>> GetAllNotifications()
        {
            return await _notificationRepository.GetAllEntity();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(int id)
        {
            var result = await _notificationRepository.GetEntity(id);
            if (result is null)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
