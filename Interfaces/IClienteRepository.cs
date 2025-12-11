
using WebApiFinal.Models;
using WebApiFinal.DTOs;

namespace WebApiFinal.Interfaces
{
    public interface IClienteRepository : IGenericRepository<Cliente>
    {
        Task<Cliente?> GetClienteWithReservasAsync(int id);

        // Métodos que usan DTOs para crear/actualizar
        Task<Cliente> CreateAsync(ClienteCreateDto dto);
        Task<Cliente?> UpdateAsync(int id, ClienteUpdateDto dto);
    }
}
