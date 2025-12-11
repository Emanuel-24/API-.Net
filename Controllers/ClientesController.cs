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
    public class ClienteController : BaseController<Cliente>
    {
        private readonly IClienteRepository _clienteRepo;

        public ClienteController(IClienteRepository clienteRepo) : base(clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClienteCreateDto dto)
        {
            var nuevo = await _clienteRepo.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = nuevo.ClienteId }, nuevo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ClienteUpdateDto dto)
        {
            var actualizado = await _clienteRepo.UpdateAsync(id, dto);
            if (actualizado == null) return NotFound();
            return Ok(actualizado);
        }

        [HttpGet("{id}/reservas")]
        public async Task<IActionResult> GetWithReservas(int id)
        {
            var cliente = await _clienteRepo.GetClienteWithReservasAsync(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }
    }
}
