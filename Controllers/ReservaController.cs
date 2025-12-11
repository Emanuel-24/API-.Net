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
    public class ReservaController : BaseController<Reserva>
    {
        private readonly IReservaRepository _repo;
        public ReservaController(IReservaRepository repo) : base(repo) => _repo = repo;

        [HttpPost]
        public async Task<IActionResult> Create(ReservaCreateDto dto)
        {
            var nueva = await _repo.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = nueva.ReservaId }, nueva);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ReservaUpdateDto dto)
        {
            var actualizado = await _repo.UpdateAsync(id, dto);
            if (actualizado == null) return NotFound();
            return Ok(actualizado);
        }

        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> GetByCliente(int clienteId)
        {
            var list = await _repo.GetReservasByClienteAsync(clienteId);
            return Ok(list);
        }

        [HttpGet("{id}/detalle")]
        public async Task<IActionResult> GetFull(int id)
        {
            var reserva = await _repo.GetReservaCompletaAsync(id);
            if (reserva == null) return NotFound();
            return Ok(reserva);
        }
        [HttpGet("completas")]
        public async Task<IActionResult> GetAllReservas()
        {
            var reservas = await _repo.GetReservasCompletasAsync();
            return Ok(reservas);
        }

    }
}
