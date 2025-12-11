

namespace WebApiFinal.Interfaces
{
    // Interfaz genérica para CRUD común en todas las entidades
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();       // Obtener todos
        Task<T?> GetByIdAsync(int id);            // Obtener por ID
        Task<T> AddAsync(T entity);               // Crear
        Task<T> UpdateAsync(T entity);            // Actualizar
        Task<bool> DeleteAsync(int id);           // Eliminar
    }
}