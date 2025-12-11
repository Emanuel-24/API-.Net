using WebApiFinal.Models;
using WebApiFinal.DTOs;

namespace WebApiFinal.Interfaces
{
    public interface IServicioRepository : IGenericRepository<Servicio>
    {
        Task<Servicio> CreateAsync(ServicioCreateDto dto);
        Task<Servicio?> UpdateAsync(int id, ServicioUpdateDto dto);
        Task<Servicio?> GetServicioWithReservasAsync(int id);
    }
}
