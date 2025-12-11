using Microsoft.EntityFrameworkCore;
using WebApiFinal.Data;
using WebApiFinal.DTOs;
using WebApiFinal.Interfaces;
using WebApiFinal.Models;
using System.Threading.Tasks;

namespace WebApiFinal.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(AppDbContext context) : base(context) { }

        public async Task<Cliente> CreateAsync(ClienteCreateDto dto)
        {
            var nuevo = new Cliente
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Telefono = dto.Telefono
            };
            _context.Clientes.Add(nuevo);
            await _context.SaveChangesAsync();
            return nuevo;
        }

        public async Task<Cliente?> UpdateAsync(int id, ClienteUpdateDto dto)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return null;
            cliente.Nombre = dto.Nombre;
            cliente.Email = dto.Email;
            cliente.Telefono = dto.Telefono;
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente?> GetClienteWithReservasAsync(int id)
        {
            return await _context.Clientes
                .Include(c => c.Reservas)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClienteId == id);
        }
    }
}
