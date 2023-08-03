using AutoMapper;
using Company.Application.Dtos.TemplateDto;
using Company.Application.Interfaces;
using Company.Model.Models;
using Company.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly IMapper _mapper;


        public TemplateController(ITemplateRepository templateRepository, IMapper mapper)
        {
            _templateRepository = templateRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Template>>> GetAllTemplates()
        {
            return await _templateRepository.GetAllEntity();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Template>> GetTemplate(int id)
        {
            var result = await _templateRepository.GetEntity(id);
            if (result is null)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Template>>> CreateTemplate(CreateTemplateDto createTemplateDto)
        {
            var temp = _mapper.Map<Template>(createTemplateDto);
            var result = await _templateRepository.AddEntity(temp, createTemplateDto.UserName);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<List<Template>>> UpdateTemplate(UpdateTemplateDto updateTemplateDto)
        {
            var temp = _mapper.Map<Template>(updateTemplateDto);
            var result = await _templateRepository.UpdateEntity(updateTemplateDto.Id, temp, updateTemplateDto.UserName);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Template>>> DeleteTemplate(int id, string userName)
        {
            var result = await _templateRepository.DeleteEntity(id, userName);
            if (result is null)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
