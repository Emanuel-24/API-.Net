using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiFinal.DTOs;
using WebApiFinal.Interfaces;
using WebApiFinal.Models;

namespace WebApiFinal.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioController : BaseController<Servicio>
    {
        private readonly IServicioRepository _servicioRepo;

        public ServicioController(IServicioRepository servicioRepo) : base(servicioRepo)
        {
            _servicioRepo = servicioRepo;
        }

        // POST: api/Servicio
        [HttpPost]
        public async Task<IActionResult> Create(ServicioCreateDto dto)
        {
            var nuevo = await _servicioRepo.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = nuevo.ServicioId }, nuevo);
        }

        // PUT: api/Servicio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ServicioUpdateDto dto)
        {
            var actualizado = await _servicioRepo.UpdateAsync(id, dto);

            if (actualizado == null)
                return NotFound();

            return Ok(actualizado);
        }

        // GET: api/Servicio/5/reservas
        [HttpGet("{id}/reservas")]
        public async Task<IActionResult> GetWithReservas(int id)
        {
            var servicio = await _servicioRepo.GetServicioWithReservasAsync(id);

            if (servicio == null)
                return NotFound();

            return Ok(servicio);
        }
    }
}
