using API_G2.Models;
using API_G2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace API_G2.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class ReservaExperienciaController : ControllerBase
    {
        private readonly ReservaService _mongoDBService;

        public ReservaExperienciaController(ReservaService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet("reservas-experiencias")]
        public async Task<List<ReservaExperiencia>> Get()
        {
            return await _mongoDBService.GetAsync();
        }

        [HttpPost("reservas-experiencias")]
        public async Task<IActionResult> Post([FromBody] ReservaExperiencia reservaExperiencia)
        {
            await _mongoDBService.CreateAsync(reservaExperiencia);
            return Created(nameof(Get), new { id = reservaExperiencia.id, reservaExperiencia });
        }

        [HttpDelete("reservas-experiencias/id")]
        public async Task<IActionResult> Delete(string id){
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }
    }
}
