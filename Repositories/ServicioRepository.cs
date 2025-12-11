using Microsoft.EntityFrameworkCore;
using WebApiFinal.Data;
using WebApiFinal.DTOs;
using WebApiFinal.Interfaces;
using WebApiFinal.Models;
using System.Threading.Tasks;

namespace WebApiFinal.Repositories
{
    public class ServicioRepository : GenericRepository<Servicio>, IServicioRepository
    {
        public ServicioRepository(AppDbContext context) : base(context) { }

        public async Task<Servicio> CreateAsync(ServicioCreateDto dto)
        {
            var nuevo = new Servicio
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio
            };
            _context.Servicios.Add(nuevo);
            await _context.SaveChangesAsync();
            return nuevo;
        }

        public async Task<Servicio?> UpdateAsync(int id, ServicioUpdateDto dto)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio == null) return null;
            servicio.Nombre = dto.Nombre;
            servicio.Descripcion = dto.Descripcion;
            servicio.Precio = dto.Precio;
            await _context.SaveChangesAsync();
            return servicio;
        }

        public async Task<Servicio?> GetServicioWithReservasAsync(int id)
        {
            return await _context.Servicios
                .Include(s => s.ServiciosReservas)
                    .ThenInclude(sr => sr.Reserva)
                .FirstOrDefaultAsync(s => s.ServicioId == id);
        }
    }
}
