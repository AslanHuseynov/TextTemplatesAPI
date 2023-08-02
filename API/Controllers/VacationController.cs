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

        public VacationController(IVacationRepository vacationRepository)
        {
            _vacationRepository = vacationRepository;
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
        public async Task<ActionResult<List<Vacation>>> CreateVacation(Vacation vacation)
        {
            var result = await _vacationRepository.AddEntity(vacation);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Vacation>>> UpdateVacation(int id, Vacation req)
        {
            var result = await _vacationRepository.UpdateEntity(id, req);
            if (result is null)
                return BadRequest(result);

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
