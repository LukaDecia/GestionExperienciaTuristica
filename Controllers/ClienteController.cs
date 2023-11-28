using API_G2.Models;
using API_G2.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API_G2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ClientesService _mongoDBService;

        public ClienteController(ClientesService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet("id/reservas-alojamientos")]
        public async Task<List<ReservaExperiencia>> Get(string id)
        {
            return await _mongoDBService.GetReservasPorClienteAsync(id);
        }
    }
}
