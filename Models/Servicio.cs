
namespace WebApiFinal.Models
{
    public class Servicio
    {
        public int ServicioId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Activo { get; set; } = true;

        // Relación muchos a muchos
        public ICollection<ServicioReserva> ServiciosReservas { get; set; }
    }
}

