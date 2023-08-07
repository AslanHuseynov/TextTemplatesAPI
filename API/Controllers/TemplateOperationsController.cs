using AutoMapper;
using Company.Application.Interfaces;
using Company.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateOperationsController : ControllerBase
    {
        private readonly ITemplateOperationsRepository _templateOperationsRepository;
        private readonly IMapper _mapper;
        private readonly ITemplateRepository _templateRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly INotificationRepository _notificationRepository;

        public TemplateOperationsController(ITemplateOperationsRepository templateOperationsRepository, IMapper mapper,
            ITemplateRepository templateRepository, IEmployeeRepository employeeRepository, INotificationRepository notificationRepository)
        {
            _templateOperationsRepository = templateOperationsRepository;
            _mapper = mapper;
            _templateRepository = templateRepository;
            _employeeRepository = employeeRepository;
            _notificationRepository = notificationRepository;
        }

        [HttpPost("SendSms")]
        public async Task<ActionResult<List<Template>>> SendSMS(string fullName, DateTime start, DateTime end, string phoneNumber, string user)
        {
            var splitName = fullName.Split(" ");
            var firstName = splitName.First();
            var lastName = splitName.Last();
            var template = await _templateRepository.GetEntity(1);
            var text = template.Text;

            //var employee = await _employeeRepository.GetEntity(employeId);

            StringBuilder result = new StringBuilder(text);

            result.Replace("@FirstName", firstName);
            result.Replace("@LastName", lastName);
            result.Replace("@From", start.ToString());
            result.Replace("@Until", end.ToString());
            result.Replace("@FirstName2", "Test");
            result.Replace("@LastName2", "Test");

            var notification = new Notification() { SavedBy = user, Text = result.ToString(), Addressee = phoneNumber };

            await _notificationRepository.AddEntity(notification);

            return Ok(result.ToString());
        }

        [HttpPost("SendMail")]
        public async Task<ActionResult<List<Template>>> SendMail(string fullName, DateTime start, DateTime end, string mail, string user)
        {
            var splitName = fullName.Split(" ");
            var firstName = splitName.First();
            var lastName = splitName.Last();
            var template = await _templateRepository.GetEntity(1);
            var text = template.Text;

            //var employee = await _employeeRepository.GetEntity(employeId);

            StringBuilder result = new StringBuilder(text);

            result.Replace("@FirstName", firstName);
            result.Replace("@LastName", lastName);
            result.Replace("@From", start.ToString());
            result.Replace("@Until", end.ToString());
            result.Replace("@FirstName2", "Test");
            result.Replace("@LastName2", "Test");

            var notification = new Notification() { SavedBy = user, Text = result.ToString(), Addressee = mail };

            await _notificationRepository.AddEntity(notification);

            return Ok(result.ToString());
            /*var result = await _templateOperationsRepository.SendMail(temp);
            return Ok(result);*/
        }
    }
}
