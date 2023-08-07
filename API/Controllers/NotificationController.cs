using AutoMapper;
using Company.Application.Dtos.NotificationDto;
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
        private readonly IMapper _mapper;

        public NotificationController(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
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

        [HttpPost]
        public async Task<ActionResult<List<Notification>>> CreateNotification(CreateNotificationDto createNotificationDto)
        {
            var notification = _mapper.Map<Notification>(createNotificationDto);
            var result = await _notificationRepository.AddEntity(notification);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<List<Notification>>> UpdateNotification(UpdateNotificationDto updateNotificationDto)
        {
            var notification = _mapper.Map<Notification>(updateNotificationDto);
            var result = await _notificationRepository.UpdateEntity(updateNotificationDto.Id, notification);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Notification>>> DeleteNotification(int id)
        {
            var result = await _notificationRepository.DeleteEntity(id);
            if (result is null)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
