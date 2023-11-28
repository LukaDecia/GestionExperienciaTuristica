using API_G2.Models;
using API_G2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API_G2.Controllers
{
    [Controller]
    //[Authorize]
    [Route("api/[controller]")]
    public class ExperienciaTuristicaController : Controller
    {
        private readonly ExperienciaService _mongoDBService;
        public ExperienciaTuristicaController(ExperienciaService mongoDBService){
            _mongoDBService = mongoDBService;
        }

        [HttpGet("experiencias")]
        public async Task<List<ExperienciaTuristica>> Get() {
            return await _mongoDBService.GetAsync();
        }

        [HttpPost("experiencias")]
        public async Task<IActionResult> Post ([FromBody] ExperienciaTuristica experienciaTuristica){
            await _mongoDBService.CreateAsync(experienciaTuristica);
            return Created(nameof(Get), new {id = experienciaTuristica.id, experienciaTuristica});
        }

        [HttpPut("experiencias/id")]
        public async Task<IActionResult> AddToExperiencia(string id, [FromBody] ExperienciaTuristica experiencia){
            try
            {
                await _mongoDBService.AddToExperienciaAsync(id, experiencia);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete("experiencias/id")]
        public async Task<IActionResult> Delete(string id){
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }
    }
}