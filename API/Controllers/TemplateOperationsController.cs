using Company.Application.Interfaces;
using Company.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateOperationsController : ControllerBase
    {
        private readonly ITemplateOperationsRepository _templateOperationsRepository;

        public TemplateOperationsController(ITemplateOperationsRepository templateOperationsRepository)
        {
            _templateOperationsRepository = templateOperationsRepository;
        }

        [HttpPost("SendSms")]
        public async Task<ActionResult<List<Template>>> SendSMS(int templateId, int employeeId, string number, string user)
        {
            var result = await _templateOperationsRepository.SendSMS(templateId, employeeId, number, user);
            return Ok(result);
        }

        [HttpPost("SendMail")]
        public async Task<ActionResult<List<Template>>> SendMail(int templateId, int employeeId, string mail, string user)
        {
            var result = await _templateOperationsRepository.SendMail(templateId, employeeId, mail, user);
            return Ok(result);
        }
    }
}
