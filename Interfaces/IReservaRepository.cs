
using WebApiFinal.Models;
using WebApiFinal.DTOs;

namespace WebApiFinal.Interfaces
{
    public interface IReservaRepository : IGenericRepository<Reserva>
    {
        Task<IEnumerable<Reserva>> GetReservasByClienteAsync(int clienteId);
        Task<Reserva> CreateAsync(ReservaCreateDto dto);
        Task<Reserva?> UpdateAsync(int id, ReservaUpdateDto dto);
        Task<Reserva?> GetReservaCompletaAsync(int id);
        Task<IEnumerable<Reserva>> GetReservasCompletasAsync();

    }
}
