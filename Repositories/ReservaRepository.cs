using Microsoft.EntityFrameworkCore;
using WebApiFinal.Data;
using WebApiFinal.DTOs;
using WebApiFinal.Interfaces;
using WebApiFinal.Models;

namespace WebApiFinal.Repositories
{
    public class ReservaRepository : GenericRepository<Reserva>, IReservaRepository
    {
        public ReservaRepository(AppDbContext context) : base(context) { }

        public async Task<Reserva> CreateAsync(ReservaCreateDto dto)
        {
            var nueva = new Reserva
            {
                ClienteId = dto.ClienteId,
                FechaReserva = dto.FechaReserva,
                Observaciones = dto.Observaciones,
                Estado = "Pendiente"
            };

            _context.Reservas.Add(nueva);
            await _context.SaveChangesAsync(); // ReservaId se genera aquí

            // Insertar relación con cada servicio
            foreach (var servicioId in dto.ServicioIds)
            {
                _context.ServiciosReservas.Add(new ServicioReserva
                {
                    ReservaId = nueva.ReservaId,
                    ServicioId = servicioId
                });
            }

            await _context.SaveChangesAsync();

            return await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.ServiciosReservas)
                .ThenInclude(sr => sr.Servicio)
                .FirstOrDefaultAsync(r => r.ReservaId == nueva.ReservaId);
        }

        public async Task<Reserva?> UpdateAsync(int id, ReservaUpdateDto dto)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) return null;

            reserva.Estado = dto.Estado;
            reserva.Observaciones = dto.Observaciones;

            await _context.SaveChangesAsync();

            // Incluir Cliente y la colección de servicios asociados
            return await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.ServiciosReservas)
                    .ThenInclude(sr => sr.Servicio)
                .FirstOrDefaultAsync(r => r.ReservaId == id);
        }

        public async Task<IEnumerable<Reserva>> GetReservasByClienteAsync(int clienteId)
        {
            return await _context.Reservas
                .Where(r => r.ClienteId == clienteId)
                .Include(r => r.ServiciosReservas)
                    .ThenInclude(sr => sr.Servicio)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Reserva?> GetReservaCompletaAsync(int id)
        {
            return await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.ServiciosReservas)
                    .ThenInclude(sr => sr.Servicio)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ReservaId == id);
        }

        public async Task<IEnumerable<Reserva>> GetReservasCompletasAsync()
        {
            return await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.ServiciosReservas)
                    .ThenInclude(sr => sr.Servicio)
                .ToListAsync();
        }
    }
}
