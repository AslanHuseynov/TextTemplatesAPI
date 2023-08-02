using AutoMapper;
using Company.Application.Dtos.VacationDto;
using Company.Application.Interfaces;
using Company.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationController : ControllerBase
    {
        private readonly IVacationRepository _vacationRepository;
        private readonly IMapper _mapper;

        public VacationController(IVacationRepository vacationRepository, IMapper mapper)
        {
            _vacationRepository = vacationRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<Vacation>>> GetAllVacations()
        {
            return await _vacationRepository.GetAllEntity();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vacation>> GetVacation(int id)
        {
            var result = await _vacationRepository.GetEntity(id);
            if (result is null)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Vacation>>> CreateVacation(CreateVacationDto createVacationDto)
        {
            var vacation = _mapper.Map<Vacation>(createVacationDto);
            var result = await _vacationRepository.AddEntity(vacation);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<List<Vacation>>> UpdateVacation(UpdateVacationDto updateVacationDto)
        {
            var vacation = _mapper.Map<Vacation>(updateVacationDto);
            var result = await _vacationRepository.UpdateEntity(updateVacationDto.Id, vacation);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Vacation>>> DeleteVacation(int id)
        {
            var result = await _vacationRepository.DeleteEntity(id);
            if (result is null)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
